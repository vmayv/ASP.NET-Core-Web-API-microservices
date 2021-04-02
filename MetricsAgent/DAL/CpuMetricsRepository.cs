using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using static ClassLibrary.Class;

namespace MetricsAgent.DAL
{
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {
        IList<CpuMetric> GetByTimePeriodPercentile(DateTimeOffset fromDate, DateTimeOffset toDate, Percentile percentile);
    }
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private SQLiteConnection _connection;

        // инжектируем соединение с базой данных в наш репозиторий через конструктор
        public CpuMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public void Create(CpuMetric item)
        {
            // создаем команду
            using var cmd = new SQLiteCommand(_connection);
            // прописываем в команду SQL запрос на вставку данных
            cmd.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(@value, @time)";

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
        /*
        public void Delete(int id)
        {
            using var cmd = new SQLiteCommand(_connection);
            // прописываем в команду SQL запрос на удаление данных
            cmd.CommandText = "DELETE FROM cpumetrics WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }
        
        public void Update(CpuMetric item)
        {
            using var cmd = new SQLiteCommand(_connection);
            // прописываем в команду SQL запрос на обновление данных
            cmd.CommandText = "UPDATE cpumetrics SET value = @value, time = @time WHERE id=@id;";
            cmd.Parameters.AddWithValue("@id", item.Id);
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }*/
        /*
        public IList<CpuMetric> GetAll()
        {
            using var cmd = new SQLiteCommand(_connection);

            // прописываем в команду SQL запрос на получение всех данных из таблицы
            cmd.CommandText = "SELECT * FROM cpumetrics";

            var returnList = new List<CpuMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    // добавляем объект в список возврата
                    returnList.Add(new CpuMetric
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        // налету преобразуем прочитанный TEXT в DateTimeOffset
                        Time = DateTimeOffset.Parse(reader.GetString(2))
                    });
                }
            }

            return returnList;
        }*/

        public IList<CpuMetric> GetByTimePeriod(DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            var fromDateLong = long.Parse(fromDate.ToString("yyyyMMddHHmmss"));
            var toDateLong = long.Parse(toDate.ToString("yyyyMMddHHmmss"));

            using var cmd = new SQLiteCommand(_connection);

            // прописываем в команду SQL запрос на получение данных
            cmd.CommandText = "SELECT * FROM cpumetrics WHERE time BETWEEN @fromDateLong AND @toDateLong";
            cmd.Parameters.AddWithValue("@fromDateLong", fromDateLong);
            cmd.Parameters.AddWithValue("@toDateLong", toDateLong);

            var returnList = new List<CpuMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    // добавляем объект в список возврата
                    returnList.Add(new CpuMetric
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

        public IList<CpuMetric> GetByTimePeriodPercentile(DateTimeOffset fromDate, DateTimeOffset toDate, Percentile percentile)
        {
            throw new NotImplementedException();
        }
    }
}

