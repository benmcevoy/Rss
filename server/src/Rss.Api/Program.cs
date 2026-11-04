using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rss.Api.Data;
using Rss.Api.Data.Services;

namespace Rss.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
            .AddResponseCaching()
            .AddCors(options => options.AddPolicy("CorsPolicy",
                builder => builder
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyHeader()))
            .AddApiVersioning(options =>
            {
                // default is 1.0 but it is clearer to specify
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();

            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            })
            .AddOpenApi();

            builder.Services.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultSQLiteConnection")));

            builder.Services
                .AddHttpLogging()
                .AddTransient<RefreshDataService>()
                .AddTransient<MarkAsReadDataService>()
                .AddTransient<FeedDataService>();

            var app = builder.Build();

            // you should also think about Cors
            app
                .UseCors("CorsPolicy")
                .UseResponseCaching();

            // you should also think about authentication

            // think about telemetry - Appinsights etc
            // i'd think about structured logging like serilog
            // I also like policy based using something like DynamicProxy
            // this is configured in appsettings.Development.json
            app.UseHttpLogging();

            var apiVersions = app.NewApiVersionSet()
                .HasApiVersion(new ApiVersion(1))
                .Build();

            // always publish this because I want to
            // default is to /openapi/v1.json
            app.MapOpenApi().WithDocumentPerVersion();

            app
                // You should have this on, generate a self signed cert etc
                // .UseHttpsRedirection();
                .MapControllers()
                .WithApiVersionSet(apiVersions);
            // you should also think about Cors


            app.Run();
        }


    }
}