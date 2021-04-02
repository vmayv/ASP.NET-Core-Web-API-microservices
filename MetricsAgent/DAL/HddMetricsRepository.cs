using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    public interface IHddMetricsRepository : IRepository<HddMetric>
    {
        HddMetric GetLast();
    }
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private SQLiteConnection _connection;

        // инжектируем соединение с базой данных в наш репозиторий через конструктор
        public HddMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }
        public void Create(HddMetric item)
        {
            // создаем команду
            using var cmd = new SQLiteCommand(_connection);
            // прописываем в команду SQL запрос на вставку данных
            cmd.CommandText = "INSERT INTO hddmetrics(value, time) VALUES(@value, @time)";

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

        public IList<HddMetric> GetByTimePeriod(DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            var fromDateLong = long.Parse(fromDate.ToString("yyyyMMddHHmmss"));
            var toDateLong = long.Parse(toDate.ToString("yyyyMMddHHmmss"));

            using var cmd = new SQLiteCommand(_connection);

            // прописываем в команду SQL запрос на получение данных
            cmd.CommandText = "SELECT * FROM hddmetrics WHERE time BETWEEN @fromDateLong AND @toDateLong";
            cmd.Parameters.AddWithValue("@fromDateLong", fromDateLong);
            cmd.Parameters.AddWithValue("@toDateLong", toDateLong);

            var returnList = new List<HddMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    // добавляем объект в список возврата
                    returnList.Add(new HddMetric
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

        public HddMetric GetLast()
        {
            using var cmd = new SQLiteCommand(_connection);

            // прописываем в команду SQL запрос на получение данных
            cmd.CommandText = "SELECT * FROM hddmetrics WHERE id = (SELECT MAX(id) from hddmetrics)";

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // если удалось что то прочитать
                if (reader.Read())
                {
                    // возвращаем прочитанное
                    return new HddMetric
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = DateTimeOffset.Parse(reader.GetString(2))
                    };
                }
                else
                {
                    // не нашлось запись по идентификатору, не делаем ничего
                    return null;
                }
            }
        }
    }
}


