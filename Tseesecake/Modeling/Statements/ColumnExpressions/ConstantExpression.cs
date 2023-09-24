using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling.Statements.ColumnExpressions
{
    internal class ConstantExpression : BaseExpression
    {
        public object Constant { get; }
        public ConstantExpression(object constant)
            => Constant = constant;
    }
}
