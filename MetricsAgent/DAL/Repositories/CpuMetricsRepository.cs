using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Dapper;
using Core.Interfaces;

namespace MetricsAgent.DAL.Repositories
{
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {
        IList<CpuMetric> GetByTimePeriodPercentile(DateTimeOffset fromDate, DateTimeOffset toDate, Percentile percentile);
    }
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        public CpuMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(CpuMetric item)
        {
            using (var connection = new SQLiteConnection(SQLParams.ConnectionString))
            {
                //  запрос на вставку данных с плейсхолдерами для параметров
                connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
                    // анонимный объект с параметрами запроса
                    new
                    {
                        // value подставится на место "@value" в строке запроса
                        // значение запишется из поля Value объекта item
                        value = item.Value,

                        // записываем в поле time количество секунд
                        time = item.Time
                    });
            }
        }
        public IList<CpuMetric> GetByTimePeriod(DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            using (var connection = new SQLiteConnection(SQLParams.ConnectionString))
            {
                return connection.Query<CpuMetric>("SELECT * FROM cpumetrics WHERE time BETWEEN @fromDateLong AND @toDateLong",
                    new
                    {
                        fromDateLong = fromDate.ToUnixTimeSeconds(),
                        toDateLong = toDate.ToUnixTimeSeconds()
                    }).ToList();
            }
        }

        public IList<CpuMetric> GetByTimePeriodPercentile(DateTimeOffset fromDate, DateTimeOffset toDate, Percentile percentile)
        {
            throw new NotImplementedException();
        }
    }
}

