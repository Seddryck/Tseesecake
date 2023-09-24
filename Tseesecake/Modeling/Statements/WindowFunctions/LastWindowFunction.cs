using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.Expressions;

namespace Tseesecake.Modeling.Statements.WindowFunctions
{
    internal class LastWindowFunction : BaseExpressionWindowFunction
    {
        public LastWindowFunction(IExpression expression)
            : base(expression) { }
    }
}
