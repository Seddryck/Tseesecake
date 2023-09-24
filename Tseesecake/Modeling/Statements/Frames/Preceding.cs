using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.ColumnExpressions;

namespace Tseesecake.Modeling.Statements.Frames
{
    internal class Preceding : Boundary
    {
        public Preceding(IExpression expression)
            : base(expression, "preceding") { }
    }
}
