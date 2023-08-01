﻿using Sprache;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Parsing.Query.Calculator
{
    /// <summary>
	/// Simple calculator grammar.
	/// Supports arithmetic operations and parentheses.
	/// </summary>
    internal class SimpleCalculator
    {
        protected internal virtual Parser<string> DecimalWithoutLeadingDigits =>
            from dot in Parse.Char('.')
            from fraction in Parse.Number
            select dot + fraction;

        protected internal virtual Parser<string> DecimalWithLeadingDigits =>
            Parse.Number.Then(n => DecimalWithoutLeadingDigits.XOr(Parse.Return(string.Empty)).Select(f => n + f));

        protected internal virtual Parser<string> Decimal =>
            DecimalWithLeadingDigits.XOr(DecimalWithoutLeadingDigits);

        protected internal virtual Parser<Expression> Constant =>
            Decimal.Select(x => Expression.Constant(double.Parse(x, CultureInfo.InvariantCulture))).Named("Constant");

        protected internal static Parser<ExpressionType> Operator(string op, ExpressionType opType) =>
            Parse.String(op).Token().Return(opType);

        protected internal virtual Parser<ExpressionType> Add =>
            Operator("+", ExpressionType.AddChecked);

        protected internal virtual Parser<ExpressionType> Subtract =>
            Operator("-", ExpressionType.SubtractChecked);

        protected internal virtual Parser<ExpressionType> Multiply =>
            Operator("*", ExpressionType.MultiplyChecked);

        protected internal virtual Parser<ExpressionType> Divide =>
            Operator("/", ExpressionType.Divide);

        protected internal virtual Parser<ExpressionType> Modulo =>
            Operator("%", ExpressionType.Modulo);

        protected internal virtual Parser<ExpressionType> Power =>
            Operator("^", ExpressionType.Power);

        protected virtual Parser<Expression> ExpressionInParentheses =>
            from lparen in Parse.Char('(')
            from expr in Expr
            from rparen in Parse.Char(')')
            select expr;

        protected internal virtual Parser<Expression> Factor =>
            ExpressionInParentheses.XOr(Constant);

        protected internal virtual Parser<Expression> NegativeFactor =>
            from sign in Parse.Char('-')
            from factor in Factor
            select Expression.NegateChecked(factor);

        protected internal virtual Parser<Expression> Operand =>
            (NegativeFactor.XOr(Factor)).Token();

        protected internal virtual Parser<Expression> InnerTerm =>
            Parse.ChainRightOperator(Power, Operand, Expression.MakeBinary);

        protected internal virtual Parser<Expression> Term =>
            Parse.ChainOperator(Multiply.Or(Divide).Or(Modulo), InnerTerm, Expression.MakeBinary);

        protected internal Parser<Expression> Expr =>
            Parse.ChainOperator(Add.Or(Subtract), Term, Expression.MakeBinary);

        public virtual Expression ParseExpression(string text) =>
            Expr.Parse(text);
    }
}
