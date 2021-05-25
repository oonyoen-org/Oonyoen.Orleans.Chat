using Oonyoen.Orleans.Chat.Abstractions;
using Orleans;
using Orleans.Streams;
using System;
using System.Threading.Tasks;

namespace Oonyoen.Orleans.Chat
{
    public class ChatChannel : Grain, IChatChannel
    {
        private IAsyncStream<ChatMessage> messageStream;

        public override Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider(Constants.ChatStreamProvider);
            messageStream = streamProvider.GetStream<ChatMessage>(Guid.NewGuid(), Constants.ChatStreamNamespace);
            return base.OnActivateAsync();
        }

        public async Task<Guid> Join()
        {
            return messageStream.Guid;
        }

        public async Task<Guid> Leave()
        {
            return messageStream.Guid;
        }

        public async Task SendMessage(ChatMessage message)
        {
            await messageStream.OnNextAsync(message);
        }
    }
}
