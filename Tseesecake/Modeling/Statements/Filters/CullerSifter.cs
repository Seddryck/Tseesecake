using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Modeling.Statements.Filters
{
    internal class CullerSifter : GathererSifter
    {
        public override string Template { get => nameof(CullerSifter); }

        public CullerSifter(Measurement measurement, Func<Expression, Expression, BinaryExpression> comparison, object value)
            : base(measurement, comparison, value) { }
    }
}
