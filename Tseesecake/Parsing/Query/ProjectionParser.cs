using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying.Expressions;
using Tseesecake.Querying.Projections;

namespace Tseesecake.Parsing.Query
{
    internal class ProjectionParser
    {
        protected internal static Parser<IProjection> Column =
            from identifier in Grammar.Identifier
            select new ColumnProjection(new ColumnReference(identifier));

        protected internal static Parser<IProjection> Expression =
            from expression in ExpressionParser.Expression
            from _ in Keyword.As
            from alias in Grammar.Identifier
            select new ExpressionProjection(expression, alias);

        public static Parser<IProjection> Aggregation =
            from aggregation in AggregationParser.Aggregation
            from _ in Keyword.As
            from alias in Grammar.Identifier
            select new AggregationProjection(aggregation, null, alias);

        public static Parser<IProjection> Projection = Aggregation.Or(Column).Or(Expression);
    }
}
