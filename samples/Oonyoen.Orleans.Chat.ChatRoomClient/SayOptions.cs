using CommandLine;

namespace Oonyoen.Orleans.Chat.ChatRoomClient
{
    [Verb("say", HelpText = "Send a message to the current channel.")]
    public class SayOptions
    {
        [Option('m', "message", Required = true, HelpText = "The message you want to send.")]
        public string Message { get; set; }
    }
}
