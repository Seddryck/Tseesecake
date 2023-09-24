using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.Expressions;

namespace Tseesecake.Modeling.Statements.WindowFunctions
{
    internal class FirstWindowFunction : BaseExpressionWindowFunction
    {
        public FirstWindowFunction(IExpression expression)
            : base(expression) { }
    }
}
