using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers.Action;
using Tseesecake.Modeling;
using Tseesecake.Modeling.Statements.Expressions;

namespace Tseesecake.Modeling.Statements.WindowFunctions
{
    internal abstract class BaseExpressionWindowFunction : BaseWindowFunction
    {
        protected BaseExpressionWindowFunction(IExpression expression)
            => Expression = expression;

        public IExpression Expression { get; set; }

        public virtual void Accept(IActionArranger arranger)
        {
            if (arranger is IActionArranger<IExpression> arr)
                Expression = arr.Execute(Expression);

            if (Expression is IArrangeable arrangeable)
                arrangeable.Accept(arranger);
        }
    }
}
