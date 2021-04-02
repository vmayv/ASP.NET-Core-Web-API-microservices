using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using static ClassLibrary.Class;

namespace MetricsAgent.DAL
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetric>
    {
    }
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private SQLiteConnection _connection;

        // инжектируем соединение с базой данных в наш репозиторий через конструктор
        public DotNetMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public void Create(DotNetMetric item)
        {
            // создаем команду
            using var cmd = new SQLiteCommand(_connection);
            // прописываем в команду SQL запрос на вставку данных
            cmd.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)";

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

        public IList<DotNetMetric> GetByTimePeriod(DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            var fromDateLong = long.Parse(fromDate.ToString("yyyyMMddHHmmss"));
            var toDateLong = long.Parse(toDate.ToString("yyyyMMddHHmmss"));

            using var cmd = new SQLiteCommand(_connection);

            // прописываем в команду SQL запрос на получение данных
            cmd.CommandText = "SELECT * FROM dotnetmetrics WHERE time BETWEEN @fromDateLong AND @toDateLong";
            cmd.Parameters.AddWithValue("@fromDateLong", fromDateLong);
            cmd.Parameters.AddWithValue("@toDateLong", toDateLong);

            var returnList = new List<DotNetMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    // добавляем объект в список возврата
                    returnList.Add(new DotNetMetric
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        // налету преобразуем прочитанный int в DateTimeOffset
                        Time = DateTimeOffset.Parse(reader.GetString(2))
                    });
                }
            }

            return returnList;
        }
    }
}
