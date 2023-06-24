using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.Filters
{
    internal class Culler : Gatherer
    {
        public override string Template { get => nameof(Culler); }

        public Culler(Measurement measurement, Func<Expression, Expression, BinaryExpression> comparison, object value)
            : base(measurement, comparison, value) { }
    }
}
