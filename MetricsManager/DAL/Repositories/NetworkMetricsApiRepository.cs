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
    public interface INetworkMetricsApiRepository : IRepositoryApi<NetworkMetricApi>
    {
    }

    public class NetworkMetricsApiRepository : INetworkMetricsApiRepository
    {
        public NetworkMetricsApiRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(NetworkMetricApi item)
        {
            using (var connection = new SQLiteConnection(SQLParams.ConnectionString))
            {
                connection.Execute("INSERT INTO networkmetrics(agentid, value, time) VALUES(@agentid, @value, @time)",
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
            using (var connection = new SQLiteConnection(SQLParams.ConnectionString))
            {
                return connection.QuerySingle("SELECT MAX(Time) FROM networkmetrics WHERE agentId = @agentId", new { agentId = agentId });
            }
        }

        public NetworkMetricApi GetMetricbyPercentileFromDatabase(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime, Percentile percentile)
        {
            throw new NotImplementedException();
        }


        public IList<NetworkMetricApi> GetMetricFromDatabase(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(SQLParams.ConnectionString))
            {
                return connection.Query<NetworkMetricApi>("SELECT id, value, time, agentid FROM networkmetrics WHERE agentid=@agentId AND time BETWEEN @fromTime AND @toTime", new { agentId = agentId, fromTime = fromTime.Ticks, toTime = toTime.Ticks }).ToList();
            }
        }
    }
}
