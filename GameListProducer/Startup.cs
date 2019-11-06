using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QueueSender;
using RequestsCreator;
using System.IO;

namespace GameListProducer
{
    public class Startup
    {
        public Startup()
        {
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var config = BuildConfiguration();

            // register all the sections that we want to inject
            RegisterOptions(services, config);
            RegisterClasses(services);

            services.AddHostedService<TimedHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Service is running !!!");
                });
            });
        }

        private IConfigurationRoot BuildConfiguration()
        {
            var configBuilder = new ConfigurationBuilder()
                                               .SetBasePath(Directory.GetCurrentDirectory())
                                               .AddJsonFile("appsettings.json");
            var config = configBuilder.Build();
            return config;
        }

        private void RegisterClasses(IServiceCollection services)
        {
            services.AddSingleton<IRequestCreator, GameListRequestCreator>();
            services.AddSingleton<IGamesProcessor, GameListProcessor>();
            services.AddSingleton<IQueueSender, RabbitMQSender>();
        }

        private void RegisterOptions(IServiceCollection services, IConfigurationRoot config)
        {
            services.Configure<GameListRequestConfig>(config.GetSection("GameListRequestConfig"));
            services.Configure<RabbitMQSenderConfiguration>(config.GetSection("RabbitMQSenderConfiguration"));
        }
    }
}
