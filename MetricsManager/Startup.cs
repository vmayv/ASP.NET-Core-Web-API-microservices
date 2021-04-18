using FluentMigrator.Runner;
using MetricsManager.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager
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
            services.AddControllers();
            services.AddSingleton<ICpuMetricsApiRepository, CpuMetricsApiRepository>();
            services.AddSingleton<INetworkMetricsApiRepository, NetworkMetricsApiRepository>();
            services.AddSingleton<IGcHeapSizeMetricsApiRepository, GcHeapSizeMetricsApiRepository>();
            services.AddSingleton<IRamMetricsApiRepository, RamMetricsApiRepository>();
            services.AddSingleton<IHddMetricsApiRepository, HddMetricsApiRepository>();

            services.AddHttpClient();

            services.AddFluentMigratorCore()
                 .ConfigureRunner(rb => rb
        // ��������� ��������� SQLite 
        .AddSQLite()
        // ������������� ������ �����������
        .WithGlobalConnectionString(SQLParams.ConnectionString)
        // ������������ ��� ������ ������ � ����������
        .ScanIn(typeof(Startup).Assembly).For.Migrations()
    ).AddLogging(lb => lb
        .AddFluentMigratorConsole());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            });
        }
    }
}
