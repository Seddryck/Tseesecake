using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Parsing
{
    internal class Predicate
    {
        public static readonly Parser<string> After = Parse.IgnoreCase("After").Text().Token();
        public static readonly Parser<string> Before = Parse.IgnoreCase("Before").Text().Token();
        public static readonly Parser<string> Range = Parse.IgnoreCase("Range").Text().Token();
        public static readonly Parser<string> Since = Parse.IgnoreCase("Since").Text().Token();

        public static readonly Parser<string> Temporizer = After.Or(Before).Or(Range).Or(Since);

        public static readonly Parser<string> Equal = Parse.IgnoreCase("Is").Text().Token().Return("Equal");
        public static readonly Parser<string> Different = Parse.IgnoreCase("Is").Text().Token().Then(_ => Parse.IgnoreCase("Not").Text().Token().Return("Different"));
        public static readonly Parser<string> In = Parse.IgnoreCase("In").Text().Token().Return("In");

        public static readonly Parser<string> NonArrayDicer = Different.Or(Equal);
        public static readonly Parser<string> ArrayDicer = In;

        public static readonly Parser<Func<Expression, Expression, BinaryExpression>> EqualOperator = Parse.IgnoreCase("=").Text().Token().Return((Func<Expression, Expression, BinaryExpression>)Expression.Equal);
        public static readonly Parser<Func<Expression, Expression, BinaryExpression>> GreaterThanOperator = Parse.IgnoreCase(">").Text().Token().Return((Func<Expression, Expression, BinaryExpression>)Expression.GreaterThan);
        public static readonly Parser<Func<Expression, Expression, BinaryExpression>> GreaterThanOrEqualOperator = Parse.IgnoreCase(">=").Text().Token().Return((Func<Expression, Expression, BinaryExpression>)Expression.GreaterThanOrEqual);
        public static readonly Parser<Func<Expression, Expression, BinaryExpression>> LessThanOperator = Parse.IgnoreCase("<").Text().Token().Return((Func<Expression, Expression, BinaryExpression>)Expression.LessThan);
        public static readonly Parser<Func<Expression, Expression, BinaryExpression>> LessThanOrEqualOperator = Parse.IgnoreCase("<=").Text().Token().Return((Func<Expression, Expression, BinaryExpression>)Expression.LessThanOrEqual);

        public static readonly Parser<Func<Expression, Expression, BinaryExpression>> Operator =
            LessThanOrEqualOperator
            .Or(GreaterThanOrEqualOperator)
            .Or(EqualOperator)
            .Or(GreaterThanOperator)
            .Or(LessThanOperator);
    }
}
