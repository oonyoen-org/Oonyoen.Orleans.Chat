using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oonyoen.Orleans.Chat.Abstractions;
using Oonyoen.Orleans.ClientExtensions;
using Orleans;
using Orleans.Hosting;

namespace Oonyoen.Orleans.Chat.Client
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseChatClient(this IHostBuilder builder)
        {
            return builder
                .UseOrleansClient(builder =>
                {
                    builder
                        .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(IChatChannel).Assembly).WithReferences())
                        .AddSimpleMessageStreamProvider(Constants.ChatStreamProvider);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IChatChannelSubscriber, ChatChannelSubscriber>();
                    services.AddSingleton<IChatMessageSender, ChatMessageSender>();
                });
        }
    }
}
