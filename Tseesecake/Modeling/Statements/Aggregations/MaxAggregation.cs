using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements.Expressions;

namespace Tseesecake.Modeling.Statements.Aggregations
{
    internal class MaxAggregation : BaseAggregation
    {
        public MaxAggregation(Column column)
            : base(new ColumnReference(column.Name)) { }
        public MaxAggregation(IExpression expression)
            : base(expression) { }
    }
}
