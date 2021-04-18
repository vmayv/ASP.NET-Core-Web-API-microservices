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
    public interface ICpuMetricsApiRepository : IRepositoryApi<CpuMetricApi>
    {
    }

    public class CpuMetricsApiRepository : ICpuMetricsApiRepository
    {
        public CpuMetricsApiRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(CpuMetricApi item)
        {
            using (var connection = new SQLiteConnection(SQLParams.ConnectionString))
            {
                connection.Execute("INSERT INTO cpumetrics(agentid, value, time) VALUES(@agentid, @value, @time)",
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
                try
                {
                    var response = connection.QuerySingle<CpuMetricApi>("SELECT MAX(Time) FROM cpumetrics WHERE agentId = @agentId", new { agentId = agentId });
                    return response.Time;
                }
                catch
                {
                    return DateTimeOffset.UnixEpoch;
                }
            }
        }

        public CpuMetricApi GetMetricbyPercentileFromDatabase(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime, Percentile percentile)
        {
            throw new NotImplementedException();
        }


        public IList<CpuMetricApi> GetMetricFromDatabase(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(SQLParams.ConnectionString))
            {
                return connection.Query<CpuMetricApi>("SELECT id, value, time, agentid FROM cpumetrics WHERE agentid=@agentId AND time BETWEEN @fromTime AND @toTime", new { agentId = agentId, fromTime = fromTime.Ticks, toTime = toTime.Ticks }).ToList();
            }
        }
    }
}
