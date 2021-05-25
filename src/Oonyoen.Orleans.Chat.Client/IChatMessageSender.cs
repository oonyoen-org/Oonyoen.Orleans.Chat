using Oonyoen.Orleans.Chat.Abstractions;
using System.Threading.Tasks;

namespace Oonyoen.Orleans.Chat.Client
{
    public interface IChatMessageSender
    {
        string DefaultChannel { get; set; }
        Task SendMessage(ChatMessage message, string channelName = null);
    }
}
