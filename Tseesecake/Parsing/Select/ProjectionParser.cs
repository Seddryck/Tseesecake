﻿using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Projections;

namespace Tseesecake.Parsing.Select
{
    internal class ProjectionParser
    {
        protected internal static Parser<IProjection> Column =
            from identifier in Grammar.Identifier
            select new Projection(new ColumnReference(identifier), identifier);

        protected internal static Parser<IProjection> Expression =
            from expression in ExpressionParser.Expression
            from _ in Keyword.As
            from alias in Grammar.Identifier
            select new Projection(expression, alias);

        public static Parser<IProjection> Aggregation =
            from aggregation in AggregationParser.Aggregation
            from _ in Keyword.As
            from alias in Grammar.Identifier
            select new Projection(new AggregationExpression(aggregation), alias);

        public static Parser<IProjection> Window =
            from windowExpression in WindowParser.WindowExpression
            from _ in Keyword.As
            from alias in Grammar.Identifier
            select new Projection(windowExpression, alias);

        public static Parser<IProjection> Projection = Window.Or(Aggregation).Or(Column).Or(Expression);
    }
}