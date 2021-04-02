using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetric>
    {

    }
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private SQLiteConnection _connection;

        // инжектируем соединение с базой данных в наш репозиторий через конструктор
        public NetworkMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }
        public void Create(NetworkMetric item)
        {
            throw new NotImplementedException();
        }

        public IList<NetworkMetric> GetAll()
        {
            throw new NotImplementedException();
        }

        public NetworkMetric GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<NetworkMetric> GetByTimePeriod(DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            throw new NotImplementedException();
        }
    }
}
