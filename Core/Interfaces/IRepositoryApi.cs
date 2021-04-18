using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IRepositoryApi<T> where T : class
    {
        void Create(T item);

        IList<T> GetMetricFromDatabase(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime);

        T GetMetricbyPercentileFromDatabase(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime, Percentile percentile);

       // IList<T> GetMetricfromClusterfromDatabase(DateTimeOffset fromTime, DateTimeOffset toTime);

      //  T GetMetricbyPercentilefromClusterfromDatabase(DateTimeOffset fromTime, DateTimeOffset toTime, Percentile percentile);

        DateTimeOffset GetLastTime(int agentId);
    }
}
