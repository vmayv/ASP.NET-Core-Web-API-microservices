using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SqlController : ControllerBase
    {
        [HttpGet("sql-test")]
        public IActionResult TryToSqlLite()
        {
            string connectionString = "Data Source=:memory:";
            string sqlText = "SELECT SQLITE_VERSION()";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using var cmd = new SQLiteCommand(sqlText, connection);
                string version = cmd.ExecuteScalar().ToString();

                return Ok(version);
            }

        }
    }
}
