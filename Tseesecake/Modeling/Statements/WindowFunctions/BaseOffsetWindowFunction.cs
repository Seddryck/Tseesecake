using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Modeling.Statements.Expressions;

namespace Tseesecake.Modeling.Statements.WindowFunctions
{
    internal abstract class BaseOffsetWindowFunction : BaseExpressionWindowFunction
    {
        protected BaseOffsetWindowFunction(IExpression expression, IExpression offset, IExpression @default)
            : base(expression) { (Offset, Default) = (offset, @default); }

        public IExpression Offset { get; }
        public IExpression Default { get; }
    }
}
