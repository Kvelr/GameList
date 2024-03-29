using System.IO;
using GameListCommon;
using GameListDal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QueueReceiver;

namespace GameListConsumer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var config = BuildConfiguration();
            RegisterOptions(services, config);
            RegisterClasses(services);

            services.AddDbContext<GameListDBContext>(options =>
            options.UseSqlServer(config.GetConnectionString("GamesDBConnection")));

            var sp = services.BuildServiceProvider();
            var queueReceiver = sp.GetService<IQueueReceiver>();
            queueReceiver.ReciveMessage();
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
                    await context.Response.WriteAsync("GameListConsumer is running!");
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
            services.AddScoped<IQueueReceiver, RabbitMQReceiver>();
            services.AddScoped<GamesDal>();          
        }

        private void RegisterOptions(IServiceCollection services, IConfigurationRoot config)
        {
            services.Configure<RabbitMQReceiverConfig>(config.GetSection("RabbitMQReceiverConfig"));
        }

    }
}
