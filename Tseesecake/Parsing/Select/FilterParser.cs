using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.Filters;

namespace Tseesecake.Parsing.Select
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
                , arguments.Cast<Modeling.Statements.Expressions.ConstantExpression>().Select(x => x.Constant).ToArray()
            );

        protected internal static Parser<IFilter> ArrayDicer =
            from identifier in Grammar.Identifier
            from predicate in Predicate.ArrayDicer
            from arguments in Arguments
            select Factory.Instantiate(
                "Dicer"
                , predicate
                , identifier
                , new object[] { arguments.Cast<Modeling.Statements.Expressions.ConstantExpression>().Select(x => x.Constant).Cast<string>().ToArray() }
            );

        protected internal static Parser<IFilter> NonArrayDicer =
            from identifier in Grammar.Identifier
            from predicate in Predicate.NonArrayDicer
            from arguments in Arguments
            select Factory.Instantiate(
                "Dicer"
                , predicate
                , identifier
                , arguments.Cast<Modeling.Statements.Expressions.ConstantExpression>().Select(x => x.Constant).ToArray()
            );

        protected internal static Parser<IFilter> Dicer = NonArrayDicer.Or(ArrayDicer);

        private record struct SifterInternal(string Identifier, Func<Expression, Expression, BinaryExpression> Operator, object[] Parameters) 
        { 
            public object[] Arguments
            {
                get
                {
                    var args = Parameters.ToList().Cast<object>().ToList();
                    args.Insert(0, Operator);
                    return args.ToArray();
                }
            }
        }

        private static Parser<SifterInternal> BaseSifter =
            from identifier in Grammar.Identifier
            from @operator in Predicate.Operator
            from arguments in Grammar.Numeric.Select(x => new object[] { x })
            select new SifterInternal(
                identifier
                , @operator
                , arguments.ToArray()
            );

        protected internal static Parser<IFilter> Gatherer =
            from sifter in BaseSifter
            select Factory.Instantiate(
                "Sifter"
                , "Gatherer"
                , sifter.Identifier
                , sifter.Arguments
            );

        protected internal static Parser<IFilter> Culler =
            from _ in Keyword.Not
            from sifter in Parse.Contained(BaseSifter, Parse.Char('('), Parse.Char(')'))
            select Factory.Instantiate(
                "Sifter"
                , "Culler"
                , sifter.Identifier
                , sifter.Arguments
            );

        protected internal static Parser<IFilter> Sifter = Culler.Or(Gatherer);

        public static Parser<IFilter> Filter = Temporizer.Or(Dicer).Or(Sifter);
    }
}
