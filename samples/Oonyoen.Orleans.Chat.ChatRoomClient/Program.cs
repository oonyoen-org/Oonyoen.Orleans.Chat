using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Oonyoen.CommandLineParser.AspNetCore;
using Oonyoen.Orleans.Chat.Client;
using Oonyoen.Orleans.ClientExtensions;
using Orleans;
using Orleans.Configuration;
using System.Threading.Tasks;

namespace Oonyoen.Orleans.Chat.ChatRoomClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseChatClient()
                .UseOrleansClient(builder =>
                {
                    builder.UseLocalhostClustering();
                    builder.Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = "dev";
                        options.ServiceId = "OrleansBasics";
                    });
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IVerbHandler<SayOptions>, SayHandler>();
                    services.AddTransient<IVerbHandler<JoinOptions>, JoinHandler>();
                    services.AddTransient<IVerbHandler<LeaveOptions>, LeaveHandler>();
                })
                .UseInteractiveCommandLine(options =>
                {
                    options.AddVerb<SayOptions>();
                    options.AddVerb<JoinOptions>();
                    options.AddVerb<LeaveOptions>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                });
    }
}
