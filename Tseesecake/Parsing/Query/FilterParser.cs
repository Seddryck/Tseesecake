using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying.Expressions;
using Tseesecake.Querying.Filters;
using Tseesecake.Querying.Projections;

namespace Tseesecake.Parsing.Query
{
    internal class FilterParser
    {
        private static readonly Lazy<FilterFactory> lazy = new Lazy<FilterFactory>(() => new FilterFactory());
        protected static FilterFactory Factory { get { return lazy.Value; } }

        private static Parser<IEnumerable<object>> SingleArgument =
                ExpressionParser.Constant.Select(x => new[] { x });

        private static Parser<IEnumerable<object>> ManyArgument =
            from lpar in Parse.Char('(').Token()
            from args in ExpressionParser.Constant.DelimitedBy(Parse.Char(','))
            from rpar in Parse.Char(')').Token()
            select args;

        protected internal static Parser<IEnumerable<object>> Arguments =
            ManyArgument.Or(SingleArgument);

        protected internal static Parser<IFilter> Temporizer =
            from identifier in Grammar.Identifier
            from predicate in Predicate.Temporizer
            from arguments in Arguments
            select Factory.Instantiate(
                "Temporizer"
                , predicate
                , identifier
                , arguments.Cast<ConstantExpression>().Select(x => x.Constant).ToArray()
            );

        protected internal static Parser<IFilter> Dicer =
            from identifier in Grammar.Identifier
            from predicate in Predicate.Dicer
            from arguments in Arguments
            select Factory.Instantiate(
                "Dicer"
                , predicate
                , identifier
                , arguments.Cast<ConstantExpression>().Select(x => x.Constant).ToArray()
            );

        public static Parser<IFilter> Filter = Temporizer.Or(Dicer);
    }
}
