﻿using System;
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
    internal class GathererSifter : Sifter
    {
        protected Func<Expression, Expression, BinaryExpression> Comparison { get; }
        public string ComparisonOperator { get => Comparison.GetMethodInfo().Name; }
        public virtual object Value { get; private set; }

        public override string Template { get => nameof(GathererSifter); }
        public GathererSifter(IExpression expression, Func<Expression, Expression, BinaryExpression> comparison, object value)
            : base(expression) { (Comparison, Value) = (comparison, value); }
    }
}
