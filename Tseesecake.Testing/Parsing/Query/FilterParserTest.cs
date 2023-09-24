using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.Filters;
using Tseesecake.Parsing.Query;

namespace Tseesecake.Testing.Parsing.Query
{
    public class FilterParserTest
    {
        [Test]
        [TestCase("Instant AFTER TIMESTAMP '2023-01-01'", typeof(AfterTemporizer))]
        [TestCase("Instant BEFORE TIMESTAMP '2023-01-01'", typeof(BeforeTemporizer))]
        [TestCase("Instant RANGE (TIMESTAMP '2023-01-01', TIMESTAMP '2023-02-01')", typeof(RangeTemporizer))]
        [TestCase("Instant SINCE INTERVAL '12:00:00'", typeof(SinceTemporizer))]
        public void Parse_Temporizer_CorrectValue(string text, Type type)
        {
            var filter = FilterParser.Filter.Parse(text);
            Assert.That(filter, Is.Not.Null);
            Assert.That(filter, Is.TypeOf(type));
        }

        [Test]
        [TestCase("Producer IS 'WindEnergy'", typeof(EqualDicer))]
        [TestCase("Producer IS NOT 'WindEnergy'", typeof(DifferentDicer))]
        [TestCase("Producer IN ('WindEnergy', 'EnergyCo')", typeof(InDicer))]
        public void Parse_Dicer_CorrectValue(string text, Type type)
        {
            var filter = FilterParser.Filter.Parse(text);
            Assert.That(filter, Is.Not.Null);
            Assert.That(filter, Is.TypeOf(type));
        }

        [Test]
        [TestCase("Forecasted = 10", typeof(GathererSifter))]
        [TestCase("Forecasted > 10", typeof(GathererSifter))]
        [TestCase("Forecasted >= 10", typeof(GathererSifter))]
        [TestCase("Forecasted < 10", typeof(GathererSifter))]
        [TestCase("Forecasted <= 10", typeof(GathererSifter))]
        [TestCase("NOT(Forecasted = 10)", typeof(CullerSifter))]
        public void Parse_Sifter_CorrectValue(string text, Type type)
        {
            var filter = FilterParser.Filter.Parse(text);
            Assert.That(filter, Is.Not.Null);
            Assert.That(filter, Is.TypeOf(type));
        }
    }
}
