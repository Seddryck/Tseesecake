using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Modeling.Statements.ColumnExpressions
{
    public class ColumnReference : IReference<Column>, IColumn, IExpression
    {
        public string Name { get; }

        public string Template => "ColumnExpression";

        public ColumnReference(string name)
            => Name = name;
    }
}
