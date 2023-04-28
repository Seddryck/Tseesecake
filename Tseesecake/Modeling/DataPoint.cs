using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling
{
    public class DataPoint : Column
    {
        public DataPoint(string name)
            : base(name, DbType.Decimal) { }
    }
}
