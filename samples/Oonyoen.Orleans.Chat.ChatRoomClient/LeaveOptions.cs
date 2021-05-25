using CommandLine;

namespace Oonyoen.Orleans.Chat.ChatRoomClient
{
    [Verb("leave", HelpText = "Leaves a channel.")]
    public class LeaveOptions
    {
        [Option('c', "channel", Required = true, HelpText = "The name of the channel to leave.")]
        public string ChannelName { get; set; }
    }
}
