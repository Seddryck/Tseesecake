using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying.Expressions;

namespace Tseesecake.Querying.WindowFunctions
{
    internal class LagWindowFunction : BaseOffsetWindowFunction
    {
        public LagWindowFunction(IExpression expression, IExpression offset, IExpression @default)
            : base(expression, offset, @default) { }
    }
}
