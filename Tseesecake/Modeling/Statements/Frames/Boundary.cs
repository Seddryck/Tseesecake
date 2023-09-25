using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.Expressions;

namespace Tseesecake.Modeling.Statements.Frames
{
    internal class Boundary
    {
        public IExpression? Value { get; }
        public string Position { get; }

        public Boundary(IExpression? value, string position)
            => (Value, Position) = (value, position);
    }
}
