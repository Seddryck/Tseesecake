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
    internal class ScientificCalculatorTest
    {
        [Test]
        [TestCase("0b11 + 0b101", 8)]
        [TestCase("(0b11 + 0b101) * (0xa + 0x12)", 8*(10+18))]
        [TestCase("2e3 - 1e2", 1900)]
        [TestCase("2e3 - 1e-2", 1999.99)]
        public void Parse_Expression_CorrectEvaluation(string value, double expected)
        {
            var expr = new ScientificCalculator().ParseExpression(value);
            Assert.That(expr, Is.Not.Null);
            var lambda = Expression.Lambda<Func<double>>(expr).Compile();
            Assert.That(lambda.Invoke(), Is.EqualTo(expected));
        }
    }
}
