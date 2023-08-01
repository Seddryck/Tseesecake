using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying.Expressions;

namespace Tseesecake.Querying.WindowFunctions
{
    internal abstract class BaseExpressionWindowFunction : BaseWindowFunction, IExpressionWindowFunction
    {
        protected BaseExpressionWindowFunction(IExpression expression)
            => Expression = expression;

        public IExpression Expression { get; set; }
    }
}
