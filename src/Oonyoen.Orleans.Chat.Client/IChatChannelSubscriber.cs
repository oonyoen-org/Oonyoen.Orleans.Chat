using Oonyoen.Orleans.Chat.Abstractions;
using Orleans.Streams;
using System.Threading.Tasks;

namespace Oonyoen.Orleans.Chat.Client
{
    public interface IChatChannelSubscriber
    {
        Task JoinAsync(string channelName, IAsyncObserver<ChatMessage> observer);
        Task LeaveAsync(string channelName);
    }
}
