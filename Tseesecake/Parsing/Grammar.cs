using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Tseesecake.Parsing
{
    internal class Grammar
    {
        public static readonly Parser<string> Textual = Parse.Letter.AtLeastOnce().Text().Token();
        public static readonly Parser<string> DoubleQuotedTextual = Parse.CharExcept("\"").AtLeastOnce().Text().Contained(Parse.Char('\"'), Parse.Char('\"')).Token();
        public static readonly Parser<string> SingleQuotedTextual = Parse.CharExcept("\'").AtLeastOnce().Text().Contained(Parse.Char('\''), Parse.Char('\'')).Token();
        public static readonly Parser<string> BacktickQuotedTextual = Parse.CharExcept("`").AtLeastOnce().Text().Contained(Parse.Char('`'), Parse.Char('`')).Token();

        public static readonly Parser<bool> True = Parse.IgnoreCase("True").Text().Token().Return(true);
        public static readonly Parser<bool> False = Parse.IgnoreCase("False").Text().Token().Return(false);
        public static readonly Parser<bool> Boolean = True.XOr(False);

        public static readonly Parser<DateTime> Timestamp = 
            from typeFixer in Parse.IgnoreCase("Timestamp").Text().Token()
            from value in SingleQuotedTextual.Text().Token()
            select DateTime.Parse(value);

        public static readonly Parser<TimeSpan> Interval =
            from typeFixer in Parse.IgnoreCase("Interval").Text().Token()
            from value in SingleQuotedTextual.Text().Token()
            select TimeSpan.Parse(value);

        public static readonly Parser<decimal> Numeric = 
            from op in Parse.Char('-').Optional()
            from dec in Parse.DecimalInvariant.Token()
            select decimal.Parse(dec, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat) * (op.IsDefined ? -1 : 1);

        public static readonly Parser<string> Identifier = Textual.Or(DoubleQuotedTextual);

        public static readonly Parser<char> Terminator = Parse.Char(';').Token();
    }
}
