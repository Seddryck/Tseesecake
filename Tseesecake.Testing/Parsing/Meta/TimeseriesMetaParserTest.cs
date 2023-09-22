using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements;
using Tseesecake.Parsing.Meta;

namespace Tseesecake.Testing.Parsing.Meta
{
    internal class TimeseriesMetaParserTest
    {
        [Test]
        public virtual void Parse_SimpleTimeseries_Valid()
        {
            var text = "SHOW TIMESERIES WindEnergy";
            var statement = TimeseriesMetaParser.Show.Parse(text);
            Assert.That(statement, Is.Not.Null);
            Assert.That(statement, Is.TypeOf<ShowFieldsTimeseries>());
            var showFields = (statement as ShowFieldsTimeseries)!;
            Assert.That(showFields.TimeseriesName, Is.EqualTo("WindEnergy"));
        }

        [Test]
        public virtual void Parse_SimpleTimeseriesWithoutFacet_Valid()
        {
            var text = "SHOW TIMESERIES";
            var statement = TimeseriesMetaParser.Show.Parse(text);
            Assert.That(statement, Is.Not.Null);
            Assert.That(statement, Is.TypeOf<ShowAllTimeseries>());
        }
    }
}
