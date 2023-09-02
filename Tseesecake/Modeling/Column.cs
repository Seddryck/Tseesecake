using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling
{
    public abstract class Column : ColumnReference
    {
        public DbType DbType { get; }
        public string Family { get => GetType().Name; }

        public Column(string name, DbType dbType)
            : base(name) { DbType = dbType; }
    }
}
