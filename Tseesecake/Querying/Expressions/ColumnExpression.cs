using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying.Projections;

namespace Tseesecake.Querying.Expressions
{
    internal class ColumnExpression : BaseExpression
    {
        public Column Column { get; }
        public ColumnExpression(Column column)
            => Column = column;
    }
}
