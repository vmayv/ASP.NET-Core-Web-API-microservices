using MetricsAgent.DAL;
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
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
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
            ConfigureSqlLiteConnection(services);
            services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddScoped<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();
            services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
        }

        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            //string connectionString = "Data Source=:memory:";
            var connection = new SQLiteConnection(SQLParams.ConnectionString);
            connection.Open();
            PrepareSchema(connection);
            services.AddSingleton(connection);
        }

        private void PrepareSchema(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(connection))
            {
                // задаем новый текст команды для выполнения
                // удаляем таблицу с метриками если она существует в базе данных
                command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
                // отправляем запрос в базу данных
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY,
                    value INT, time INT64)";
                command.ExecuteNonQuery();


                command.CommandText = "DROP TABLE IF EXISTS dotnetmetrics";
                // отправляем запрос в базу данных
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE dotnetmetrics(id INTEGER PRIMARY KEY,
                    value INT, time INT)"; ;
                // отправляем запрос в базу данных
                command.ExecuteNonQuery();


                command.CommandText = "DROP TABLE IF EXISTS hddmetrics";
                // отправляем запрос в базу данных
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE hddmetrics(id INTEGER PRIMARY KEY,
                    value INT, time INT)"; ;
                // отправляем запрос в базу данных
                command.ExecuteNonQuery();


                command.CommandText = "DROP TABLE IF EXISTS networkmetrics";
                // отправляем запрос в базу данных
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE networkmetrics(id INTEGER PRIMARY KEY,
                    value INT, time INT)"; ;
                // отправляем запрос в базу данных
                command.ExecuteNonQuery();


                command.CommandText = "DROP TABLE IF EXISTS rammetrics";
                // отправляем запрос в базу данных
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE rammetrics(id INTEGER PRIMARY KEY,
                    value INT, time INT)"; ;
                // отправляем запрос в базу данных
                command.ExecuteNonQuery();
            }
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
