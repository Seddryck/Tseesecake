using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling
{
    public class Measurement : Column
    {
        public Measurement(string name)
            : base(name, DbType.Decimal) { }
    }
}
