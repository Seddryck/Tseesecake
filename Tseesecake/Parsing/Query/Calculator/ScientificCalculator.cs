using Sprache;
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
    /// Scientific calculator grammar.
    /// Supports binary and hexadecimal numbers and exponential notation
    /// </summary>
    internal class ScientificCalculator : SimpleCalculator
    {
        protected internal virtual Parser<string> Binary =>
            Parse.IgnoreCase("0b").Then(x =>
                Parse.Chars("01").AtLeastOnce().Text()).Token();

        protected internal virtual Parser<string> Hexadecimal =>
            Parse.IgnoreCase("0x").Then(x =>
                Parse.Chars("0123456789ABCDEFabcdef").AtLeastOnce().Text()).Token();

        protected internal virtual ulong ConvertBinary(string bin)
        {
            return bin.Aggregate(0ul, (result, c) =>
            {
                if (c < '0' || c > '1')
                {
                    throw new ParseException(bin + " cannot be parsed as binary number");
                }

                return result * 2 + c - '0';
            });
        }

        protected internal virtual ulong ConvertHexadecimal(string hex)
        {
            if (ulong.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var result))
                return result;

            throw new ParseException(hex + " cannot be parsed as hexadecimal number");
        }

        protected internal virtual Parser<string> Exponent =>
            Parse.Chars("Ee").Then(e => Parse.Number.Select(n => "e+" + n).XOr(
                Parse.Chars("+-").Then(s => Parse.Number.Select(n => "e" + s + n))));

        protected internal override Parser<string> Decimal =>
            from d in base.Decimal
            from e in Exponent.Optional()
            select d + e.GetOrElse(string.Empty);

        protected internal override Parser<Expression> Constant =>
            Hexadecimal.Select(x => Expression.Constant((double)ConvertHexadecimal(x)))
                .Or(Binary.Select(b => Expression.Constant((double)ConvertBinary(b))))
                .Or(base.Constant);
    }
}
