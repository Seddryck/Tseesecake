using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Parsing.Query;
using Tseesecake.Querying.Expressions;

namespace Tseesecake.Testing.Parsing.Query
{
    public class ExpressionParserTest
    {
        [Test]
        [TestCase("'Hello'", "Hello")]
        //[TestCase("'He''o'", "He'o")]
        public void Parse_ConstantText_CorrectValue(string value, string expected)
            => Assert.That(((ConstantExpression)ExpressionParser.Constant.Parse(value)).Constant, Is.EqualTo(expected));

        [Test]
        [TestCase("10", 10)]
        //[TestCase("10.25", 10.25)]
        //[TestCase("-10", -10)]
        //[TestCase("-10.25", -10.25)]
        public void Parse_ConstantNumeric_CorrectValue(string value, decimal expected)
            => Assert.That(((ConstantExpression)ExpressionParser.Constant.Parse(value)).Constant, Is.EqualTo(expected));

        [Test]
        [TestCase("true", true)]
        [TestCase("false", false)]
        public void Parse_ConstantBoolean_CorrectValue(string value, bool expected)
            => Assert.That(((ConstantExpression)ExpressionParser.Constant.Parse(value)).Constant, Is.EqualTo(expected));

        [Test]
        [TestCase("TIMESTAMP '2023-07-02 13:08:00'", "2023-07-02 13:08:00")]
        [TestCase("TIMESTAMP '2023-07-02'", "2023-07-02")]
        public void Parse_ConstantTimestamp_CorrectValue(string value, DateTime expected)
            => Assert.That(((ConstantExpression)ExpressionParser.Constant.Parse(value)).Constant, Is.EqualTo(expected));

        [Test]
        [TestCase("INTERVAL '3.12:00:00'", "3.12:00:00")]
        [TestCase("INTERVAL '00:05:00'", "00:05:00")]
        public void Parse_ConstantInterval_CorrectValue(string value, TimeSpan expected)
            => Assert.That(((ConstantExpression)ExpressionParser.Constant.Parse(value)).Constant, Is.EqualTo(expected));

        [Test]
        [TestCase("INTERVAL 'alpha'")]
        [TestCase("TIMESTAMP 'alpha'")]
        [TestCase("INTERVAL '4.10.25'")]
        [TestCase("TIMESTAMP '2023-15-55'")]
        public void Parse_ConstantInterval_Exception(string value)
            => Assert.Throws<FormatException>(() => ExpressionParser.Constant.Parse(value));
    }
}
