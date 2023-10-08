using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Arrangers.Action;
using Tseesecake.Modeling.Statements.Expressions;

namespace Tseesecake.Modeling.Statements.Aggregations
{
    internal abstract class BaseAggregation : IAggregation
    {
        public string Name => GetType().Name.Replace("Aggregation", "").ToLowerInvariant();
        public IExpression Expression { get; set; }

        public BaseAggregation(IExpression expression)
            => Expression = expression;

        public void Accept(IActionArranger arranger)
        {
            if (arranger is IActionArranger<IExpression> arr)
                Expression = arr.Execute(Expression);

            if (Expression is IArrangeable arrangeable)
                arrangeable.Accept(arranger);
        }
    }
}
