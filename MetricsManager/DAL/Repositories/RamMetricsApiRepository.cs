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
    public interface IRamMetricsApiRepository : IRepositoryApi<RamMetricApi>
    {
    }

    public class RamMetricsApiRepository : IRamMetricsApiRepository
    {
        public RamMetricsApiRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(RamMetricApi item)
        {
            using (var connection = new SQLiteConnection(SQLParams.ConnectionString))
            {
                connection.Execute("INSERT INTO rammetrics(agentid, value, time) VALUES(@agentid, @value, @time)",
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
                    var response = connection.QuerySingle<RamMetricApi>("SELECT MAX(Time) FROM rammetrics WHERE agentId = @agentId", new { agentId = agentId });
                    return response.Time;
                }
                catch
                {
                    return DateTimeOffset.UnixEpoch;
                }
            }
        }

        public RamMetricApi GetMetricbyPercentileFromDatabase(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime, Percentile percentile)
        {
            throw new NotImplementedException();
        }


        public IList<RamMetricApi> GetMetricFromDatabase(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(SQLParams.ConnectionString))
            {
                return connection.Query<RamMetricApi>("SELECT id, value, time, agentid FROM rammetrics WHERE agentid=@agentId AND time BETWEEN @fromTime AND @toTime", new { agentId = agentId, fromTime = fromTime.ToUnixTimeSeconds(), toTime = toTime.ToUnixTimeSeconds() }).ToList();
            }
        }
    }
}
