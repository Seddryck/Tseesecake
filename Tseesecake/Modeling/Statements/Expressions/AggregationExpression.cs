using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Arrangers.Action;
using Tseesecake.Modeling.Statements.Aggregations;

namespace Tseesecake.Modeling.Statements.Expressions
{
    internal class AggregationExpression : BaseExpression, IArrangeable
    {
        public IAggregation Aggregation { get; }
        public AggregationExpression(IAggregation aggregation)
            => (Aggregation) = (aggregation);

        public void Accept(IActionArranger arranger) 
        {
            if (arranger is IActionArranger<IExpression> exp)
                Aggregation.Expression = exp.Execute(Aggregation.Expression);
            (Aggregation.Expression as IArrangeable)?.Accept(arranger);
        }
    }
}
