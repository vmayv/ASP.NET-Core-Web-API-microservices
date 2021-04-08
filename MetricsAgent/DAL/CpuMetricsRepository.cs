using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using static ClassLibrary.Class;
using Dapper;

namespace MetricsAgent.DAL
{
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {
        IList<CpuMetric> GetByTimePeriodPercentile(DateTimeOffset fromDate, DateTimeOffset toDate, Percentile percentile);
        IList<CpuMetric> GetAll();
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
            using (var connection = new SQLiteConnection(SQLParams.connectionString))
            {
                //  запрос на вставку данных с плейсхолдерами для параметров
                connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
                    // анонимный объект с параметрами запроса
                    new
                    {
                        // value подставится на место "@value" в строке запроса
                        // значение запишется из поля Value объекта item
                        value = item.Value,

                        // записываем в поле time количество секунд
                        time = item.Time.Ticks
                    });
            }
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
                        // налету преобразуем прочитанный int в DateTimeOffset
                        Time = new DateTimeOffset(reader.GetInt64(2), TimeSpan.FromHours(3))
                    });
                }
            }

            return returnList;
        }

        public IList<CpuMetric> GetByTimePeriod(DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            using var cmd = new SQLiteCommand(_connection);

            // прописываем в команду SQL запрос на получение данных
            cmd.CommandText = "SELECT * FROM cpumetrics WHERE time BETWEEN @fromDateLong AND @toDateLong";
            cmd.Parameters.AddWithValue("@fromDateLong", fromDate.Ticks);
            cmd.Parameters.AddWithValue("@toDateLong", toDate.Ticks);

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
                        Time = new DateTimeOffset(reader.GetInt64(2), TimeSpan.FromHours(3))
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

