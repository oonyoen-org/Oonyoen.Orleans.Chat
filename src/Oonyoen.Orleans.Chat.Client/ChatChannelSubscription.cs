using Oonyoen.Orleans.Chat.Abstractions;
using Orleans.Streams;

namespace Oonyoen.Orleans.Chat.Client
{
    public class ChatChannelSubscription
    {
        public string ChannelName { get; set; }
        public StreamSubscriptionHandle<ChatMessage> Handle { get; set; }
    };
}
