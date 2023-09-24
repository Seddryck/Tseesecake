using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.ColumnExpressions;

namespace Tseesecake.Modeling.Statements.WindowFunctions
{
    internal class LeadWindowFunction : BaseOffsetWindowFunction
    {
        public LeadWindowFunction(IExpression expression, IExpression offset, IExpression @default)
            : base(expression, offset, @default) { }
    }
}
