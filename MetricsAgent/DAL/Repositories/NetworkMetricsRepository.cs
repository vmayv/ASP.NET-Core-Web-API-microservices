using Dapper;
using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;

namespace MetricsAgent.DAL.Repositories
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetric>
    {
    }
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        public NetworkMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        public void Create(NetworkMetric item)
        {
            using (var connection = new SQLiteConnection(SQLParams.ConnectionString))
            {
                //  запрос на вставку данных с плейсхолдерами для параметров
                connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
                    // анонимный объект с параметрами запроса
                    new
                    {
                        // value подставится на место "@value" в строке запроса
                        // значение запишется из поля Value объекта item
                        value = item.Value,

                        // записываем в поле time количество секунд
                        time = item.Time.ToUnixTimeSeconds()
                    });
            }
        }

        public IList<NetworkMetric> GetByTimePeriod(DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            using (var connection = new SQLiteConnection(SQLParams.ConnectionString))
            {
                return connection.Query<NetworkMetric>("SELECT * FROM networkmetrics WHERE time BETWEEN @fromDateLong AND @toDateLong",
                    new
                    {
                        fromDateLong = fromDate.ToUnixTimeSeconds(),
                        toDateLong = toDate.ToUnixTimeSeconds()
                    }).ToList();
            }
        }
    }
}
