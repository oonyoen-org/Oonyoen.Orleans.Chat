using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Oonyoen.Orleans.Chat.Silo;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

namespace Oonyoen.Orleans.Chat.ChatRoomServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseChatSilo()
                .UseOrleans(builder =>
                {
                    builder.UseLocalhostClustering();
                    builder.Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = "dev";
                        options.ServiceId = "OrleansBasics";
                    });
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                });
    }
}
