using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Filters;
using Tseesecake.Modeling.Statements.Frames;
using Tseesecake.Parsing;
using Tseesecake.Parsing.Select;

namespace Tseesecake.Testing.Parsing.Select
{
    public class FrameParserTest
    {
        [Test]
        [TestCase("RANGE BETWEEN 10 PRECEDING AND UNBOUNDED FOLLOWING", typeof(RangeBetween))]
        [TestCase("RANGE BETWEEN UNBOUNDED PRECEDING AND 20 FOLLOWING", typeof(RangeBetween))]
        [TestCase("RANGE BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW", typeof(RangeBetween))]
        [TestCase("ROWS BETWEEN 10 PRECEDING AND UNBOUNDED FOLLOWING", typeof(RowsBetween))]
        [TestCase("ROWS BETWEEN UNBOUNDED PRECEDING AND 20 FOLLOWING", typeof(RowsBetween))]
        [TestCase("ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW", typeof(RowsBetween))]
        [TestCase("ROWS 10 PRECEDING", typeof(RowsSingle))]
        [TestCase("ROWS UNBOUNDED PRECEDING", typeof(RowsSingle))]
        [TestCase("ROWS 10 FOLLOWING", typeof(RowsSingle))]
        [TestCase("ROWS UNBOUNDED FOLLOWING", typeof(RowsSingle))]
        [TestCase("ROWS CURRENT ROW", typeof(RowsSingle))]
        public void Parse_Frame_CorrectValue(string text, Type type)
            => Assert.That(FrameParser.Frame.Parse(text), Is.TypeOf(type));

        [TestCase("ROWS 10 PRECEDING", typeof(Preceding))]
        [TestCase("ROWS UNBOUNDED PRECEDING", typeof(UnboundedPreceding))]
        [TestCase("ROWS 10 FOLLOWING", typeof(Following))]
        [TestCase("ROWS UNBOUNDED FOLLOWING", typeof(UnboundedFollowing))]
        [TestCase("ROWS CURRENT ROW", typeof(CurrentRow))]
        public void Parse_FrameBoundary_CorrectValue(string text, Type type)
        {
            var frame = FrameParser.Frame.Parse(text);
            Assert.That(frame, Is.TypeOf<RowsSingle>());
            Assert.That(((RowsSingle)frame).Single, Is.TypeOf(type));
        }

        [TestCase("ROWS 10 PRECEDING", 10)]
        [TestCase("ROWS 20 FOLLOWING", 20)]
        public void Parse_FrameBoundaryConstant_CorrectValue(string text, int constant)
        {
            var frame = FrameParser.Frame.Parse(text);
            Assert.That(frame, Is.TypeOf<RowsSingle>());
            Assert.That(((RowsSingle)frame).Single, Is.InstanceOf<Boundary>());
            Assert.That(((RowsSingle)frame).Single.Value, Is.TypeOf<ConstantExpression>());
            Assert.That(((ConstantExpression)((RowsSingle)frame).Single.Value).Constant, Is.EqualTo(constant));
        }
    }
}
