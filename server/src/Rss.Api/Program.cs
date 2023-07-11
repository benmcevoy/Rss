using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Rss.Api
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        // TODO: should be from config
                        .UseKestrel()
                        // TODO: should be from config
                        // port lower than 1024 needs NET_ADMIN so use a high port
                        .UseUrls("http://api.rss.local:5000");

                });
    }
}