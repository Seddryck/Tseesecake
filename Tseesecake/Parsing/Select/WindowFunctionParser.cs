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
using Tseesecake.Modeling.Statements.WindowFunctions;

namespace Tseesecake.Parsing.Select
{
    internal class WindowFunctionParser
    {
        private static Parser<IExpression> EmptyArgument =
            from lpar in Parse.Char('(').Token()
            from rpar in Parse.Char(')').Token()
            select new LiteralExpression(string.Empty);

        private static Parser<IExpression> SingleArgument =
            from lpar in Parse.Char('(').Token()
            from expression in ExpressionParser.Expression
            from rpar in Parse.Char(')').Token()
            select expression;

        private static Parser<IExpression[]> ThreeArgument =
            from lpar in Parse.Char('(').Token()
            from first in ExpressionParser.Expression
            from _1 in Parse.Char(',').Token()
            from second in ExpressionParser.Expression
            from _2 in Parse.Char(',').Token()
            from third in ExpressionParser.Expression
            from rpar in Parse.Char(')').Token()
            select new[] { first, second, third };

        protected internal static Parser<IWindowFunction> RowNumber =
            from _ in Parse.IgnoreCase("row_number").Text().Token()
            from expression in EmptyArgument
            select new RowNumberWindowFunction();

        protected internal static Parser<IWindowFunction> Rank =
            from _ in Parse.IgnoreCase("rank").Text().Token()
            from expression in EmptyArgument
            select new RankWindowFunction();

        protected internal static Parser<IWindowFunction> DenseRank =
            from _ in Parse.IgnoreCase("dense_rank").Text().Token()
            from expression in EmptyArgument
            select new DenseRankWindowFunction();

        protected internal static Parser<IWindowFunction> First =
            from _ in Parse.IgnoreCase("first").Text().Token()
            from expression in SingleArgument
            select new FirstWindowFunction(expression);

        protected internal static Parser<IWindowFunction> Last =
            from _ in Parse.IgnoreCase("last").Text().Token()
            from expression in SingleArgument
            select new LastWindowFunction(expression);

        protected internal static Parser<IWindowFunction> Lag =
            from _ in Parse.IgnoreCase("lag").Text().Token()
            from args in ThreeArgument
            select new LagWindowFunction(args[0], args[1], args[2]);

        protected internal static Parser<IWindowFunction> Lead =
            from _ in Parse.IgnoreCase("lead").Text().Token()
            from args in ThreeArgument
            select new LeadWindowFunction(args[0], args[1], args[2]);

        public static Parser<IWindowFunction> WindowFunction =
            RowNumber.Or(Rank).Or(DenseRank).Or(First).Or(Last).Or(Lag).Or(Lead);
    }
}
