using AutoMapper;
using FluentMigrator.Runner;
using MetricsManager.DAL.Repositories;
using MetricsManager.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

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
            services.AddSingleton<IAgentsRepository, AgentsRepository>();
            services.AddSingleton<ICpuMetricsApiRepository, CpuMetricsApiRepository>();
            services.AddSingleton<INetworkMetricsApiRepository, NetworkMetricsApiRepository>();
            services.AddSingleton<IGcHeapSizeMetricsApiRepository, GcHeapSizeMetricsApiRepository>();
            services.AddSingleton<IRamMetricsApiRepository, RamMetricsApiRepository>();
            services.AddSingleton<IHddMetricsApiRepository, HddMetricsApiRepository>();


            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddHttpClient();

            services.AddFluentMigratorCore()
                 .ConfigureRunner(rb => rb
        // добавляем поддержку SQLite 
        .AddSQLite()
        // устанавливаем строку подключения
        .WithGlobalConnectionString(SQLParams.ConnectionString)
        // подсказываем где искать классы с миграциями
        .ScanIn(typeof(Startup).Assembly).For.Migrations()
    ).AddLogging(lb => lb
        .AddFluentMigratorConsole());

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            // добавляем нашу задачу
            services.AddSingleton<CpuMetricApiJob>();
            services.AddSingleton<GcHeapSizeMetricApiJob>();
            services.AddSingleton<RamMetricApiJob>();
            services.AddSingleton<HddMetricApiJob>();
            services.AddSingleton<NetworkMetricApiJob>();

            services.AddSingleton(new JobSchedule(
                jobType: typeof(CpuMetricApiJob),
                cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(
                jobType: typeof(GcHeapSizeMetricApiJob),
                cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(
                jobType: typeof(RamMetricApiJob),
                cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(
                jobType: typeof(HddMetricApiJob),
                cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(
                jobType: typeof(NetworkMetricApiJob),
                cronExpression: "0/5 * * * * ?"));
            services.AddHostedService<QuartzHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
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

            migrationRunner.MigrateUp();
        }
    }
}
