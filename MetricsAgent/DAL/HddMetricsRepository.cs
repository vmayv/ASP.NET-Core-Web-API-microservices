using Dapper;
using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    public interface IHddMetricsRepository : IRepository<HddMetric>
    {
        HddMetric GetLast();
    }
    public class HddMetricsRepository : IHddMetricsRepository
    {
        public HddMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        public void Create(HddMetric item)
        {
            using (var connection = new SQLiteConnection(SQLParams.ConnectionString))
            {
                //  запрос на вставку данных с плейсхолдерами для параметров
                connection.Execute("INSERT INTO hddmetrics(value, time) VALUES(@value, @time)",
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

        public IList<HddMetric> GetByTimePeriod(DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            using (var connection = new SQLiteConnection(SQLParams.ConnectionString))
            {
                return connection.Query<HddMetric>("SELECT * FROM hddmetrics WHERE time BETWEEN @fromDateLong AND @toDateLong",
                    new
                    {
                        fromDateLong = fromDate.Ticks,
                        toDateLong = toDate.Ticks
                    }).ToList();
            }
        }

        public HddMetric GetLast()
        {
            using (var connection = new SQLiteConnection(SQLParams.ConnectionString))
            {
                return connection.QuerySingle<HddMetric>("SELECT * FROM hddmetrics WHERE id = (SELECT MAX(id) from hddmetrics)");
            }
        }

    }
}


