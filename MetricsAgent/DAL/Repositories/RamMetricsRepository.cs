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
    public interface IRamMetricsRepository : IRepository<RamMetric>
    {
        RamMetric GetLast();
    }
    public class RamMetricsRepository : IRamMetricsRepository
    {
        public RamMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        public void Create(RamMetric item)
        {
            using (var connection = new SQLiteConnection(SQLParams.ConnectionString))
            {
                //  запрос на вставку данных с плейсхолдерами для параметров
                connection.Execute("INSERT INTO rammetrics(value, time) VALUES(@value, @time)",
                    // анонимный объект с параметрами запроса
                    new
                    {
                        // value подставится на место "@value" в строке запроса
                        // значение запишется из поля Value объекта item
                        value = item.Value,

                        // записываем в поле time количество секунд
                        time = item.Time.Ticks
                    });
            }
        }

        public IList<RamMetric> GetByTimePeriod(DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            using (var connection = new SQLiteConnection(SQLParams.ConnectionString))
            {
                return connection.Query<RamMetric>("SELECT * FROM rammetrics WHERE time BETWEEN @fromDateLong AND @toDateLong",
                    new
                    {
                        fromDateLong = fromDate.Ticks,
                        toDateLong = toDate.Ticks
                    }).ToList();
            }
        }

        public RamMetric GetLast()
        {
            using (var connection = new SQLiteConnection(SQLParams.ConnectionString))
            {
                return connection.QuerySingle<RamMetric>("SELECT * FROM rammetrics WHERE id = (SELECT MAX(id) from rammetrics)");
            }
        }
    }
}
