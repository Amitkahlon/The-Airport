using Airport_DAL.Context;
using Airport_DAL.Services;
using Airport_Server.Converter;
using Airport_Server.Hubs;
using Airport_Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Airport_Server
{
    public class Startup
    {
        private UpdateClientService updateClientService;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //todo: add interfaces.
            //todo: fix given id in plane maker

            services.AddControllers();

            services.AddSingleton(x =>
            {
                return new LogicService
                (
                    x.GetRequiredService<AirportDataService>(),
                    x.GetRequiredService<ConverterProvider>(),
                    createAirports: false
                );
            });


            services.AddSingleton<AirportDataService>();
            services.AddSingleton<ConverterProvider>();
            services.AddSingleton<UpdateClientService>();

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            this.updateClientService = app.ApplicationServices.GetService<UpdateClientService>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<AirportHub>("/airport");
            });
        }
    }
}
