using Microsoft.Extensions.Hosting;
using Oonyoen.Orleans.Chat.Abstractions;
using Orleans;
using Orleans.Hosting;

namespace Oonyoen.Orleans.Chat.Silo
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseChatSilo(this IHostBuilder builder)
        {
            return builder
                .UseOrleans(builder =>
                {
                    builder.ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(ChatChannel).Assembly).WithReferences());
                    builder.AddSimpleMessageStreamProvider(Constants.ChatStreamProvider);
                    builder.AddMemoryGrainStorage("PubSubStore");
                });
        }
    }
}
