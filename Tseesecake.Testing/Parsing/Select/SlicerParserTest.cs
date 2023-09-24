using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Parsing.Select;

namespace Tseesecake.Testing.Parsing.Select
{
    public class SlicerParserTest
    {
        [Test]
        [TestCase("Producer")]
        public void Parse_FacetSlicer_CorrectValue(string text)
        {
            var slicer = SlicerParser.Facet.Parse(text);
            Assert.That(slicer, Is.Not.Null);
        }
    }
}
