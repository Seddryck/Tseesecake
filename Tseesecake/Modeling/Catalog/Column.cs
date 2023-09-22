using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements;

namespace Tseesecake.Modeling.Catalog
{
    public abstract class Column : ICatalogItem, IColumn
    {
        public DbType DbType { get; }
        public string Family { get => GetType().Name; }

        public string Name { get; }

        public Column(string name, DbType dbType)
            => (Name, DbType) = (name, dbType);
    }
}
