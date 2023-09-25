using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.Restrictions;
using Tseesecake.Parsing.Select;

namespace Tseesecake.Testing.Parsing.Select
{
    public class RestrictionParserTest
    {
        [Test]
        [TestCase("LIMIT 20", typeof(LimitRestriction))]
        [TestCase("LIMIT 20 OFFSET 30", typeof(LimitOffsetRestriction))]
        public void Parse_Restriction_CorrectValue(string text, Type type)
        {
            var restriction = RestrictionParser.Restriction.Parse(text);
            Assert.That(restriction, Is.Not.Null);
            Assert.That(restriction, Is.TypeOf(type));
        }

        [Test]
        [TestCase("LIMIT 20", 20)]
        public void Parse_Limit_CorrectValue(string text, int expected)
        {
            var restriction = RestrictionParser.Restriction.Parse(text);
            Assert.That(restriction, Is.Not.Null);
            Assert.That(((LimitRestriction)restriction).Value, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("LIMIT 20 OFFSET 30", 30)]
        public void Parse_Offset_CorrectValue(string text, int expected)
        {
            var restriction = RestrictionParser.Restriction.Parse(text);
            Assert.That(restriction, Is.Not.Null);
            Assert.That(((LimitOffsetRestriction)restriction).Offset, Is.EqualTo(expected));
        }
    }
}
