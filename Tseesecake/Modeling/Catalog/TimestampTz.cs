using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling.Catalog
{
    public class TimestampTz : Timestamp
    {
        public TimestampTz(string name)
            : base(name, DbType.DateTimeOffset) { }
    }
}
