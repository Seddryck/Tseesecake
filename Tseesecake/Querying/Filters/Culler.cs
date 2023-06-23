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
    internal class Culler : IFilter
    {
        public Measurement Measurement { get; }

        protected Func<Expression, Expression, BinaryExpression> Comparison { get; }
        public string ComparisonOperator { get => Comparison.GetMethodInfo().Name; }
        public virtual object Value { get; private set; }

        public string Template { get => nameof(Culler); }

        public Culler(Measurement measurement, Func<Expression, Expression, BinaryExpression> comparison, object value)
            => (Measurement, Comparison, Value) = (measurement, comparison, value);
    }
}
