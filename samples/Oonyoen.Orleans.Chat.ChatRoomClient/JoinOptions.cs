using CommandLine;

namespace Oonyoen.Orleans.Chat.ChatRoomClient
{
    [Verb("join", HelpText = "Joins a channel.")]
    public class JoinOptions
    {
        [Option('c', "channel", Required = true, HelpText = "The name of the channel to join.")]
        public string ChannelName { get; set; }
    }
}
