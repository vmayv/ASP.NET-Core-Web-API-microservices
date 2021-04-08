using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    public class DateTimeOffsetHandler : SqlMapper.TypeHandler<DateTimeOffset>
    {
        public override DateTimeOffset Parse(object value)
           => new DateTimeOffset((long)value, TimeSpan.FromHours(3));

        public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
            => parameter.Value = value;

    }
}
