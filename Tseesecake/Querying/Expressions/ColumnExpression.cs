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
        public ColumnReference Reference { get; set; }
        public ColumnExpression(ColumnReference reference)
            => Reference = reference;
    }
}
