using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RequestsCreator;

namespace GameListProducer
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IRequestCreator, GameListRequestCreator>();
            services.AddSingleton<IGamesProcessor, GameListProcessor>();
            services.AddSingleton(glc => new GameListRequestConfig()
            {
                Key = Configuration.GetValue<string>("LiveScoresSettings:key"),
                Secret = Configuration.GetValue<string>("LiveScoresSettings:secret"),
                Url = Configuration.GetValue<string>("LiveScoresSettings:url"),
            });
            services.AddHttpClient();
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
    }
}
