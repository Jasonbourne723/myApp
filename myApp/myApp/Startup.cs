using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace myApp
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

            var str = "test";
            var elasticsearchOptions = new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri(Configuration["elasticEndpoint"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = "logstash-" + str + "-{0:yyyy.MM.dd}"
            };
            Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .Enrich.FromLogContext()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                    // new CompactJsonFormatter(),
                  //  .WriteTo.File(new CompactJsonFormatter(), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @$"Logs", @$"{DateTime.Now.Date.ToString("yyyyMMdd")}", @"log.txt"), rollingInterval: RollingInterval.Minute)
                    .WriteTo.Elasticsearch(elasticsearchOptions)
                    .CreateLogger();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

           // app.UseSerilog();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
