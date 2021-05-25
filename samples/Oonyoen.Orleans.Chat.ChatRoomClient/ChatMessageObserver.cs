using Microsoft.Extensions.Logging;
using Oonyoen.Orleans.Chat.Abstractions;
using Orleans.Streams;
using System;
using System.Threading.Tasks;

namespace Oonyoen.Orleans.Chat.ChatRoomClient
{
    public class ChatMessageObserver : IAsyncObserver<ChatMessage>
    {
        private readonly ILogger logger;

        public ChatMessageObserver(string channelName, ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger($"channel-{channelName}");
        }

        public Task OnCompletedAsync()
        {
            logger.LogInformation("Channel message stream closed.");
            return Task.CompletedTask;
        }

        public Task OnErrorAsync(Exception ex)
        {
            logger.LogError(ex, "Error encountered in channel message stream.");
            return Task.CompletedTask;
        }

        public Task OnNextAsync(ChatMessage item, StreamSequenceToken token = null)
        {
            logger.LogInformation(item.Text);
            return Task.CompletedTask;
        }
    }
}
