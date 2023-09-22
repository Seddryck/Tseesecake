using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Modeling.Statements.ColumnExpressions
{
    public class ColumnReference : IReference<Column>, IColumn
    {
        public string Name { get; }

        public ColumnReference(string name)
            => Name = name;
    }
}
