using Orleans;
using System;
using System.Threading.Tasks;

namespace Oonyoen.Orleans.Chat.Abstractions
{
    public interface IChatChannel : IGrainWithStringKey
    {
        /// <summary>
        /// Join the channel.
        /// </summary>
        /// <returns>The guid of the message stream associated with this channel.</returns>
        Task<Guid> Join();

        /// <summary>
        /// Leave the channel.
        /// </summary>
        /// <returns>The guid of the message stream associated with this channel.</returns>
        Task<Guid> Leave();

        /// <summary>
        /// Send a chat message to the channel.
        /// </summary>
        /// <param name="message">The message to send.</param>
        Task SendMessage(ChatMessage message);
    }
}
