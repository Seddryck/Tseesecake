using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Querying.Expressions;

namespace Tseesecake.Querying.Frames
{
    internal class Preceding : Boundary
    {
        public Preceding(IExpression expression)
            : base(expression, "preceding") { }
    }
}
