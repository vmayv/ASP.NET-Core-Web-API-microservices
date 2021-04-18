using Core;
using Core.Interfaces;
using Dapper;
using MetricsManager.DAL.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Repositories
{
    public interface IHddMetricsApiRepository : IRepositoryApi<HddMetricApi>
    {
    }

    public class HddMetricsApiRepository : IHddMetricsApiRepository
    {
        public HddMetricsApiRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(HddMetricApi item)
        {
            using (var connection = new SqliteConnection(SQLParams.ConnectionString))
            {
                connection.Execute("INSERT INTO hddmetrics(agentid, value, time) VALUES(@agentid, @value, @time)",
                    new
                    {
                        agentid = item.AgentId,
                        value = item.Value,
                        time = item.Time.Ticks
                    });
            }
        }

        public DateTimeOffset GetLastTime(int agentId)
        {
            using (var connection = new SqliteConnection(SQLParams.ConnectionString))
            {
                return connection.QuerySingle("SELECT MAX(Time) FROM hddmetrics WHERE agentId = @agentId", new { agentId = agentId });
            }
        }

        public HddMetricApi GetMetricbyPercentilefromDatabase(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime, Percentile percentile)
        {
            throw new NotImplementedException();
        }


        public IList<HddMetricApi> GetMetricfromDatabase(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SqliteConnection(SQLParams.ConnectionString))
            {
                return connection.Query<HddMetricApi>("SELECT id, value, time, agentid FROM hddmetrics WHERE agentid=@agentId AND time BETWEEN @fromTime AND @toTime", new { agentId = agentId, fromTime = fromTime.Ticks, toTime = toTime.Ticks }).ToList();
            }
        }
    }
}
