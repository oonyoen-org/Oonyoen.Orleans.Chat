using Microsoft.Extensions.Logging;
using Oonyoen.CommandLineParser.AspNetCore;
using Oonyoen.Orleans.Chat.Client;

namespace Oonyoen.Orleans.Chat.ChatRoomClient
{
    public class JoinHandler : IVerbHandler<JoinOptions>
    {
        private readonly IChatChannelSubscriber subscriber;
        private readonly IChatMessageSender messageSender;
        private readonly ILoggerFactory loggerFactory;

        public JoinHandler(
            IChatChannelSubscriber subscriber,
            IChatMessageSender messageSender,
            ILoggerFactory loggerFactory)
        {
            this.subscriber = subscriber;
            this.messageSender = messageSender;
            this.loggerFactory = loggerFactory;
        }

        public void Handle(JoinOptions result)
        {
            messageSender.DefaultChannel = result.ChannelName;
            subscriber.JoinAsync(result.ChannelName, new ChatMessageObserver(result.ChannelName, loggerFactory));
        }
    }
}