using AutoMapper;
using FluentMigrator.Runner;
using MetricsManager.Client;
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
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.IO;
using System.Reflection;

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

            services.AddSingleton<IMetricsAgentClient, MetricsAgentClient>();


            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

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

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            // ��������� ���� ������
            services.AddSingleton<CpuMetricApiJob>();
            services.AddSingleton<GcHeapSizeMetricApiJob>();
            services.AddSingleton<RamMetricApiJob>();
            services.AddSingleton<HddMetricApiJob>();
            //services.AddSingleton<NetworkMetricApiJob>();

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
            /*services.AddSingleton(new JobSchedule(
                jobType: typeof(NetworkMetricApiJob),
                cronExpression: "0/5 * * * * ?"));*/
            services.AddHostedService<QuartzHostedService>();

            services.AddSwaggerGen();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API ������� ��������� ����� ������",
                    Description = "��� ������������ API ��������� ����� ������",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "�������",
                        Email = string.Empty,
                        Url = new Uri("https://example.com/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "��� ��������!",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            migrationRunner.MigrateUp();

            // ��������� middleware � �������� ��� ��������� Swagger ��������.
            app.UseSwagger();
            // ��������� middleware ��� ��������� swagger-ui 
            // ��������� Swagger JSON �������� (���� ���������� �� ��������������� �������������
            // �� ������� ����� �������� UI).
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API ��������� ����� ������");
                c.RoutePrefix = string.Empty;
            });

        }
    }
}
