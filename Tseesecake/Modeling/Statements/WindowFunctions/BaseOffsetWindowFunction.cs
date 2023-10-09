using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers.Action;
using Tseesecake.Modeling;
using Tseesecake.Modeling.Statements.Expressions;

namespace Tseesecake.Modeling.Statements.WindowFunctions
{
    internal abstract class BaseOffsetWindowFunction : BaseExpressionWindowFunction
    {
        protected BaseOffsetWindowFunction(IExpression expression, IExpression offset, IExpression @default)
            : base(expression) { (Offset, Default) = (offset, @default); }

        public IExpression Offset { get; private set; }
        public IExpression Default { get; private set; }

        public override void Accept(IActionArranger arranger)
        {
            if (arranger is IActionArranger<IExpression> exp)
            {
                Offset = exp.Execute(Expression);
                Default = exp.Execute(Expression);
            }

            (Offset as IArrangeable)?.Accept(arranger);
            (Default as IArrangeable)?.Accept(arranger);
        }
    }
}
