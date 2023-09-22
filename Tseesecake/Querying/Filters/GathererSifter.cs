using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Querying.Filters
{
    internal class GathererSifter : Sifter
    {
        protected Func<Expression, Expression, BinaryExpression> Comparison { get; }
        public string ComparisonOperator { get => Comparison.GetMethodInfo().Name; }
        public virtual object Value { get; private set; }

        public override string Template { get => nameof(GathererSifter); }
        public GathererSifter(Measurement measurement, Func<Expression, Expression, BinaryExpression> comparison, object value)
            : base(measurement) { (Comparison, Value) = (comparison, value); }
    }
}
