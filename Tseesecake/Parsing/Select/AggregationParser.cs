using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.Aggregations;
using Tseesecake.Modeling.Statements.Expressions;

namespace Tseesecake.Parsing.Select
{
    internal class AggregationParser
    {
        private static Parser<IExpression> ArgumentExpression =
            from lpar in Parse.Char('(').Token()
            from expression in ExpressionParser.Expression
            from rpar in Parse.Char(')').Token()
            select expression;

        protected internal static Parser<IAggregation> Average =
            from _ in Parse.IgnoreCase("Avg").Text().Token()
            from expression in ArgumentExpression
            select new AverageAggregation(expression);

        protected internal static Parser<IAggregation> Count =
            from _ in Parse.IgnoreCase("Count").Text().Token()
            from expression in ArgumentExpression
            select new CountAggregation(expression);

        protected internal static Parser<IAggregation> Max =
            from _ in Parse.IgnoreCase("Max").Text().Token()
            from expression in ArgumentExpression
            select new MaxAggregation(expression);

        protected internal static Parser<IAggregation> Min =
            from _ in Parse.IgnoreCase("Min").Text().Token()
            from expression in ArgumentExpression
            select new MinAggregation(expression);

        protected internal static Parser<IAggregation> Median =
            from _ in Parse.IgnoreCase("Median").Text().Token()
            from expression in ArgumentExpression
            select new MedianAggregation(expression);

        protected internal static Parser<IAggregation> Sum =
            from _ in Parse.IgnoreCase("Sum").Text().Token()
            from expression in ArgumentExpression
            select new SumAggregation(expression);

        public static Parser<IAggregation> Aggregation =
            Average.Or(Max).Or(Min).Or(Median).Or(Count).Or(Sum);
    }
}
