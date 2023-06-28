using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Querying.Expressions;

namespace Tseesecake.Querying.WindowFunctions
{
    internal class LeadWindowFunction : BaseOffsetWindowFunction
    {
        public LeadWindowFunction(IExpression expression, IExpression offset, IExpression @default)
            : base(expression, offset, @default) { }
    }
}
