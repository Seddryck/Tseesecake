using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling.Statements.ColumnExpressions
{
    internal class LiteralExpression : BaseExpression
    {
        public string Literal { get; }
        public LiteralExpression(string literal)
            => (Literal) = (literal);
    }
}
