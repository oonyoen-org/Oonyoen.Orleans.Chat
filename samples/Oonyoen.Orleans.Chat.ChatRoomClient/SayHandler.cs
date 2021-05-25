using Oonyoen.CommandLineParser.AspNetCore;
using Oonyoen.Orleans.Chat.Abstractions;
using Oonyoen.Orleans.Chat.Client;

namespace Oonyoen.Orleans.Chat.ChatRoomClient
{
    public class SayHandler : IVerbHandler<SayOptions>
    {
        private readonly IChatMessageSender messageSender;

        public SayHandler(IChatMessageSender messageSender)
        {
            this.messageSender = messageSender;
        }

        public void Handle(SayOptions result)
        {
            messageSender.SendMessage(new ChatMessage { Text = result.Message });
        }
    }
}