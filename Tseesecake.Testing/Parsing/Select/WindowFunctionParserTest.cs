using Newtonsoft.Json.Linq;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.Aggregations;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.WindowFunctions;
using Tseesecake.Parsing.Select;

namespace Tseesecake.Testing.Parsing.Select
{
    public class WindowFunctionParserTest
    {
        [Test]
        [TestCase("row_number()", typeof(RowNumberWindowFunction))]
        [TestCase("rank()", typeof(RankWindowFunction))]
        [TestCase("dense_rank()", typeof(DenseRankWindowFunction))]
        public void Parse_EmptyArgument_WindowFunction(string text, Type type)
            => Assert.That(WindowFunctionParser.WindowFunction.Parse(text), Is.TypeOf(type));

        [Test]
        [TestCase("first", typeof(FirstWindowFunction))]
        [TestCase("last", typeof(LastWindowFunction))]
        public void Parse_SingleArgument_WindowFunction(string text, Type type)
        {
            var windowFunction = WindowFunctionParser.WindowFunction.Parse($"{text}(MyColumn)");
            Assert.That(windowFunction, Is.TypeOf(type));
            Assert.That(((BaseExpressionWindowFunction)windowFunction).Expression, Is.TypeOf<ColumnReference>());
        }

        [Test]
        [TestCase("lag", typeof(LagWindowFunction))]
        [TestCase("lead", typeof(LeadWindowFunction))]
        public void Parse_ThreeArgument_WindowFunction(string text, Type type)
        {
            var windowFunction = WindowFunctionParser.WindowFunction.Parse($"{text}(MyColumn, 3, 'myValue')");
            Assert.That(windowFunction, Is.TypeOf(type));
            Assert.That(((BaseOffsetWindowFunction)windowFunction).Expression, Is.TypeOf<ColumnReference>());
            Assert.That(((BaseOffsetWindowFunction)windowFunction).Offset, Is.TypeOf<ConstantExpression>());
            Assert.That(((BaseOffsetWindowFunction)windowFunction).Default, Is.TypeOf<ConstantExpression>());
        }

        [Test]
        [TestCase("avg", typeof(AverageAggregation))]
        [TestCase("sum", typeof(SumAggregation))]
        [TestCase("median", typeof(MedianAggregation))]
        [TestCase("count", typeof(CountAggregation))]
        [TestCase("max", typeof(MaxAggregation))]
        [TestCase("min", typeof(MinAggregation))]
        public void Parse_Aggregation_WindowFunction(string text, Type type)
        {
            var windowFunction = WindowFunctionParser.WindowFunction.Parse($"{text}(MyColumn)");
            Assert.That(windowFunction, Is.TypeOf(type));
            Assert.That(((BaseAggregation)windowFunction).Expression, Is.TypeOf<ColumnReference>());
        }
    }
}
