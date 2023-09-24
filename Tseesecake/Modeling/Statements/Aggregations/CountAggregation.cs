using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.ColumnExpressions;

namespace Tseesecake.Modeling.Statements.Aggregations
{
    internal class CountAggregation : BaseAggregation
    {
        public CountAggregation(IExpression expression)
            : base(expression) { }
    }
}
