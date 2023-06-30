using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying.Projections;

namespace Tseesecake.Querying.Expressions
{
    internal class LiteralExpression : BaseExpression
    {
        public string Literal { get; }
        public LiteralExpression(string value)
            => Literal = value;
    }
}
