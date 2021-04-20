using Core;
using Core.Interfaces;
using Dapper;
using MetricsManager.DAL.Models;
using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Repositories
{
    public interface IGcHeapSizeMetricsApiRepository : IRepositoryApi<GcHeapSizeMetricApi>
    {
    }

    public class GcHeapSizeMetricsApiRepository : IGcHeapSizeMetricsApiRepository
    {
        public GcHeapSizeMetricsApiRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(GcHeapSizeMetricApi item)
        {
            using (var connection = new SQLiteConnection(SQLParams.ConnectionString))
            {
                connection.Execute("INSERT INTO dotnetmetrics(agentid, value, time) VALUES(@agentid, @value, @time)",
                    new
                    {
                        agentid = item.AgentId,
                        value = item.Value,
                        time = item.Time.ToUnixTimeSeconds()
                    });
            }
        }

        public DateTimeOffset GetLastTime(int agentId)
        {
            using (var connection = new SQLiteConnection(SQLParams.ConnectionString))
            {
                try
                {
                    var response = connection.QuerySingle<GcHeapSizeMetricApi>("SELECT MAX(Time) FROM dotnetmetrics WHERE agentId = @agentId", new { agentId = agentId });
                    return response.Time;
                }
                catch
                {
                    return DateTimeOffset.UnixEpoch;
                }
            }
        }

        public GcHeapSizeMetricApi GetMetricbyPercentileFromDatabase(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime, Percentile percentile)
        {
            throw new NotImplementedException();
        }

        public IList<GcHeapSizeMetricApi> GetMetricFromDatabase(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(SQLParams.ConnectionString))
            {
                return connection.Query<GcHeapSizeMetricApi>("SELECT id, value, time, agentid FROM dotnetmetrics WHERE agentid=@agentId AND time BETWEEN @fromTime AND @toTime", new { agentId = agentId, fromTime = fromTime.ToUnixTimeSeconds(), toTime = toTime.ToUnixTimeSeconds() }).ToList();
            }
        }
    }
}
