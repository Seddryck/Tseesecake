using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling
{
    public abstract class Column
    {
        public string Name { get; }
        public DbType DbType { get; }

        public Column(string name, DbType dbType)
            => (Name, DbType) = (name, dbType);
    }
}
