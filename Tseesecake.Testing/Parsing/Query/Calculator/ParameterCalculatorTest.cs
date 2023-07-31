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
    internal class ParameterCalculatorTest
    {
        [Test]
        [TestCase("(10.25 + 5) * (6 / 2)", 45.75)]
        [TestCase("(0b11 + 0b101) * (0xa + 0x12)", 8*(10+18))]
        public void Parse_Expression_CorrectEvaluation(string value, double expected)
        {
            var expr = new ParameterCalculator().ParseExpression(value);
            Assert.That(expr, Is.Not.Null);
            var lambda = Expression.Lambda<Func<double>>(expr).Compile();
            Assert.That(lambda.Invoke(), Is.EqualTo(expected));
        }

        [Test]
        public void Parse_ExpressionWithTwoParameters_CorrectValue()
        {
            var expr = new ParameterCalculator().ParseExpression("Forecasted - Produced");
            Assert.That(expr, Is.Not.Null);
            Assert.That(expr.NodeType, Is.EqualTo(ExpressionType.SubtractChecked));
            Assert.That(expr, Is.AssignableTo<BinaryExpression>());
            Assert.That(((BinaryExpression)expr).Left, Is.AssignableTo<ParameterExpression>());
            Assert.That(((ParameterExpression)((BinaryExpression)expr).Left).Name, Is.EqualTo("Forecasted"));
            Assert.That(((BinaryExpression)expr).Right, Is.AssignableTo<ParameterExpression>());
            Assert.That(((ParameterExpression)((BinaryExpression)expr).Right).Name, Is.EqualTo("Produced"));
        }

        [Test]
        public void Parse_ExpressionWithParameterAndConstant_CorrectValue()
        {
            var expr = new ParameterCalculator().ParseExpression("Forecasted / 1000");
            Assert.That(expr, Is.Not.Null);
            Assert.That(expr.NodeType, Is.EqualTo(ExpressionType.Divide));
            Assert.That(expr, Is.AssignableTo<BinaryExpression>());
            Assert.That(((BinaryExpression)expr).Left, Is.AssignableTo<ParameterExpression>());
            Assert.That(((ParameterExpression)((BinaryExpression)expr).Left).Name, Is.EqualTo("Forecasted"));
            Assert.That(((BinaryExpression)expr).Right, Is.AssignableTo<ConstantExpression>());
            Assert.That(((ConstantExpression)((BinaryExpression)expr).Right).Value, Is.EqualTo(1000));
        }
    }
}
