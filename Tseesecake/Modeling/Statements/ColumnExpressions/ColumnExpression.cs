using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Modeling.Statements.ColumnExpressions
{
    internal class ColumnExpression : BaseExpression, IColumn, IExpression
    {
        public Column Column { get; }
        public string Name { get => Column.Name; }

        public ColumnExpression(Column column)
            : base() { Column = column;}
    }
}
