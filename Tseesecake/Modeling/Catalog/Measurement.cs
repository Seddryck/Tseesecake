using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling.Catalog
{
    public class Measurement : Column
    {
        public override string Template { get => "ColumnExpression"; }

        public Measurement(string name)
            : base(name, DbType.Decimal) { }
    }
}
