using System;
using System.Collections.Generic;
using System.Linq;
using Tseesecake.Querying.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Tseesecake.Modeling
{
    internal class ExpressionMeasurement : Measurement
    {
        public override string Template { get => "LiteralExpression"; }
        public IExpression Expression { get; }
        public ExpressionMeasurement(string name, IExpression expression)
            : base(name) { Expression = expression; }
    }
}
