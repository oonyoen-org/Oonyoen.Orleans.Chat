using Microsoft.Extensions.Logging;
using Oonyoen.Orleans.Chat.Abstractions;
using Orleans;
using Orleans.Streams;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Oonyoen.Orleans.Chat.Client
{
    public class ChatChannelSubscriber : IChatChannelSubscriber
    {
        private readonly IClusterClient client;
        private readonly ILogger<ChatChannelSubscriber> logger;
        private SemaphoreSlim subscriptionsLock = new(1, 1);
        private readonly List<ChatChannelSubscription> subscriptions = new();

        public ChatChannelSubscriber(
            IClusterClient client,
            ILogger<ChatChannelSubscriber> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        public async Task JoinAsync(string channelName, IAsyncObserver<ChatMessage> observer)
        {
            var channel = client.GetGrain<IChatChannel>(channelName);
            var streamId = await channel.Join();
            var stream = client.GetStreamProvider(Constants.ChatStreamProvider)
                .GetStream<ChatMessage>(streamId, Constants.ChatStreamNamespace);
            var handle = await stream.SubscribeAsync(observer);
            await subscriptionsLock.WaitAsync();
            try
            {
                subscriptions.Add(new ChatChannelSubscription
                {
                    ChannelName = channelName,
                    Handle = handle
                });
                logger.LogInformation("Subscribed to {channelName}", channelName);
            }
            finally
            {
                subscriptionsLock.Release();
            }
        }

        public async Task LeaveAsync(string channelName)
        {
            await subscriptionsLock.WaitAsync();
            try
            {
                await Task.WhenAll(subscriptions
                    .Where(subscription => subscription.ChannelName == channelName)
                    .Select(subscription => subscription.Handle.UnsubscribeAsync()));
                logger.LogInformation("Unsubscribed from {channelName}", channelName);
            }
            finally
            {
                subscriptionsLock.Release();
            }
        }
    }
}
