using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Parsing.Query;
using Tseesecake.Parsing.Query.Calculator;
using static System.Net.Mime.MediaTypeNames;

namespace Tseesecake.Testing.Parsing.Query.Calculator
{
    internal class SimpleCalculatorTest
    {
        [Test]
        [TestCase("10 + 20", 30)]
        [TestCase("10.25 - .45", 9.8)]
        [TestCase("-10 * -1", 10)]
        [TestCase("(10.25 + 5) * 3", 45.75)]
        [TestCase("3 * (10.25 + 5)", 45.75)]
        [TestCase("(10.25 + 5) * (6 / 2)", 45.75)]
        [TestCase("(2 + 1) * (10.25 + 5)", 45.75)]
        public void Parse_Expression_CorrectEvaluation(string value, double expected)
        {
            var expr = new SimpleCalculator().ParseExpression(value);
            Assert.That(expr, Is.Not.Null);
            var lambda = Expression.Lambda<Func<double>>(expr).Compile();
            Assert.That(lambda.Invoke(), Is.EqualTo(expected));
        }
    }
}
