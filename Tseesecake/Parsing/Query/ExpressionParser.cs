﻿using Sprache;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Modeling.Statements.ColumnExpressions;
using Tseesecake.Modeling.Statements.Aggregations;
using Tseesecake.Querying.Expressions;
using Tseesecake.Querying.Projections;

namespace Tseesecake.Parsing.Query
{
    internal class ExpressionParser
    {
        protected internal static Parser<IExpression> Constant =
            from value in Grammar.SingleQuotedTextual
                .Or(Grammar.Numeric.Select(x => (object)x))
                .Or(Grammar.Boolean.Select(x => (object)x))
                .Or(Grammar.Timestamp.Select(x => (object)x))
                .Or(Grammar.Interval.Select(x => (object)x))
            select new ConstantExpression(value);

        protected internal static Parser<IExpression> Literal = 
            from code in Grammar.BacktickQuotedTextual
            select new LiteralExpression(code);

        protected internal static Parser<IExpression> Column = 
            from identifier in Grammar.Identifier
            select new ColumnExpression(new ColumnReference(identifier));

        public static Parser<IExpression> Expression = Constant.Or(Literal).Or(Column);
    }
}
