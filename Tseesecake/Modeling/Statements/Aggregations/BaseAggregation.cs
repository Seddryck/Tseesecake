﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.ColumnExpressions;

namespace Tseesecake.Modeling.Statements.Aggregations
{
    internal abstract class BaseAggregation : IAggregation
    {
        public string Name => GetType().Name.Replace("Aggregation", "").ToLowerInvariant();
        public IExpression Expression { get; set; }

        public BaseAggregation(IExpression expression)
            => Expression = expression;
    }
}
