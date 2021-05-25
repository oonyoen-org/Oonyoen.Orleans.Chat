using Oonyoen.CommandLineParser.AspNetCore;
using Oonyoen.Orleans.Chat.Client;

namespace Oonyoen.Orleans.Chat.ChatRoomClient
{
    public class LeaveHandler : IVerbHandler<LeaveOptions>
    {
        private readonly IChatChannelSubscriber subscriber;

        public LeaveHandler(IChatChannelSubscriber subscriber)
        {
            this.subscriber = subscriber;
        }

        public void Handle(LeaveOptions result)
        {
            subscriber.LeaveAsync(result.ChannelName);
        }
    }
}