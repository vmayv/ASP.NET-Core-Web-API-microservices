using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    public interface IRamMetricsRepository : IRepository<RamMetric>
    {

    }
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private SQLiteConnection _connection;

        // инжектируем соединение с базой данных в наш репозиторий через конструктор
        public RamMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }
        public void Create(RamMetric item)
        {
            // создаем команду
            using var cmd = new SQLiteCommand(_connection);
            // прописываем в команду SQL запрос на вставку данных
            cmd.CommandText = "INSERT INTO rammetrics(value, time) VALUES(@value, @time)";

            // добавляем параметры в запрос из нашего объекта
            cmd.Parameters.AddWithValue("@value", item.Value);

            // в таблице будем хранить время в секундах, потому преобразуем перед записью в секунды
            // через свойство
            cmd.Parameters.AddWithValue("@time", item.Time);
            // подготовка команды к выполнению
            cmd.Prepare();

            // выполнение команды
            cmd.ExecuteNonQuery();
        }

        public IList<RamMetric> GetAll()
        {
            throw new NotImplementedException();
        }

        public RamMetric GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<RamMetric> GetByTimePeriod(DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            throw new NotImplementedException();
        }
    }
}
