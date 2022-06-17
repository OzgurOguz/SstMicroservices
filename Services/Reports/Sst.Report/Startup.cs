
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Sst.Report.App;
using Sst.Report.App.Interfaces;
using Sst.Report.Consumers;
using Sst.Report.Data.Settings;
using Sst.Report.Services;
using System;

namespace Sst.Report
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMassTransit(x =>
            {
                x.AddConsumer<ReportConsumer>();
                //x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cur =>
                //{
                //    //cur.UseHealthCheck(provider);
                //    cur.Host(new Uri("rabbitmq://localhost"), h =>
                //    {
                //        h.Username("guest");
                //        h.Password("guest");
                //    });
                //    cur.ReceiveEndpoint("report", oq =>
                //    {
                //        //oq.PrefetchCount = 20;
                //        //oq.UseMessageRetry(r => r.Interval(2, 100));
                //        oq.ConfigureConsumer<ReportConsumer>(provider);
                //    });
                //})
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(Configuration["ServiceBus:Uri"]), host =>
                    {
                        host.Username(Configuration["ServiceBus:UserName"]);
                        host.Password(Configuration["ServiceBus:Password"]);
                    });
                    cfg.ReceiveEndpoint(Configuration["ServiceBus:Queue"], e =>
                    {
                        e.ConfigureConsumer<ReportConsumer>(context);
                    });
                }

                );
            });


            services.AddScoped<ReportConsumer>();

            //services.AddOptions<MassTransitHostOptions>()
            //               .Configure(options =>
            //               {
            //                   // if specified, waits until the bus is started before
            //                   // returning from IHostedService.StartAsync
            //                   // default is false
            //                   options.WaitUntilStarted = true;

            //                   // if specified, limits the wait time when starting the bus
            //                   options.StartTimeout = TimeSpan.FromSeconds(10);

            //                   // if specified, limits the wait time when stopping the bus
            //                   options.StopTimeout = TimeSpan.FromSeconds(30);
            //               });

            services.AddScoped<IReportService, ReportService>();
            services.AddControllers();

            services.Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"));

            services.AddSingleton<IDatabaseSettings>(sp =>
            {
                return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sst.Report", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sst.Report v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
