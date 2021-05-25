using Microsoft.Extensions.Logging;
using Oonyoen.Orleans.Chat.Abstractions;
using Orleans;
using System.Threading.Tasks;

namespace Oonyoen.Orleans.Chat.Client
{
    public class ChatMessageSender : IChatMessageSender
    {
        private readonly IClusterClient client;
        private readonly ILogger<ChatMessageSender> logger;

        public string DefaultChannel { get; set; } = null;

        public ChatMessageSender(
            IClusterClient client,
            ILogger<ChatMessageSender> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        public async Task SendMessage(ChatMessage message, string channelName = null)
        {
            var targetChannel = channelName ?? DefaultChannel;
            if (targetChannel == null)
            {
                logger.LogInformation("Message not sent: no channel specified.");
                return;
            }
            var channel = client.GetGrain<IChatChannel>(targetChannel);
            await channel.SendMessage(message);
        }
    }
}
