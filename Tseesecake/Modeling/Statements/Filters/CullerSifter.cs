using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements.Expressions;

namespace Tseesecake.Modeling.Statements.Filters
{
    internal class CullerSifter : GathererSifter
    {
        public override string Template { get => nameof(CullerSifter); }

        public CullerSifter(IExpression expression, Func<Expression, Expression, BinaryExpression> comparison, object value)
            : base(expression, comparison, value) { }
    }
}
