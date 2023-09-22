using DubUrl.Mapping;
using DubUrl.Querying.Dialects;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Mounting;
using Tseesecake.Parsing.Dml;

namespace Tseesecake.Testing.Parsing.Dml
{
    internal class DmlParserTest
    {
        [Test]
        public virtual void Parse_SimpleTimeseries_Valid()
        {
            var text = "CREATE OR REPLACE TIMESERIES WindEnergy(Instant TIMESTAMP, WindPark FACET, Producer FACET, Forecasted MEASUREMENT, Produced MEASUREMENT)";
            var ts = DmlParser.Timeseries.Parse(text);
            Assert.That(ts, Is.Not.Null);
            Assert.That(ts.Name, Is.EqualTo("WindEnergy"));
            Assert.That(ts.Timestamp.Name, Is.EqualTo("Instant"));
            Assert.That(ts.Facets.Count, Is.EqualTo(2));
            Assert.That(ts.Facets.Select(x => x.Name), Does.Contain("WindPark"));
            Assert.That(ts.Facets.Select(x => x.Name), Does.Contain("Producer"));
            Assert.That(ts.Measurements.Count, Is.EqualTo(2));
            Assert.That(ts.Measurements.Select(x => x.Name), Does.Contain("Forecasted"));
            Assert.That(ts.Measurements.Select(x => x.Name), Does.Contain("Produced"));
        }

        [Test]
        public virtual void Parse_SimpleTimeseriesWithoutFacet_Valid()
        {
            var text = "CREATE OR REPLACE TIMESERIES WindEnergy(Instant TIMESTAMP, Forecasted MEASUREMENT, Produced MEASUREMENT)";
            var ts = DmlParser.Timeseries.Parse(text);
            Assert.That(ts, Is.Not.Null);
            Assert.That(ts.Name, Is.EqualTo("WindEnergy"));
            Assert.That(ts.Timestamp.Name, Is.EqualTo("Instant"));
            Assert.That(ts.Facets, Is.Empty);
            Assert.That(ts.Measurements.Count, Is.EqualTo(2));
            Assert.That(ts.Measurements.Select(x => x.Name), Does.Contain("Forecasted"));
            Assert.That(ts.Measurements.Select(x => x.Name), Does.Contain("Produced"));
        }

        [Test]
        public virtual void Parse_SimpleFileTimeseries_Valid()
        {
            var text = "CREATE OR REPLACE TIMESERIES WindEnergy(Instant TIMESTAMP, Forecasted MEASUREMENT) IMPORT FROM FILE '.\\..\\..\\..\\WindEnergy.csv'";
            var ts = DmlParser.Timeseries.Parse(text);
            Assert.That(ts, Is.Not.Null);
            Assert.That(ts.Name, Is.EqualTo("WindEnergy"));
            Assert.That(ts.Timestamp.Name, Is.EqualTo("Instant"));
            Assert.That(ts.Facets, Is.Empty);
            Assert.That(ts.Measurements.Count, Is.EqualTo(1));
            Assert.That(ts.Measurements.First().Name, Is.EqualTo("Forecasted"));

            Assert.That(ts, Is.TypeOf<FileTimeseries>());
            var fileTs = (FileTimeseries)ts;
            Assert.That(fileTs.File, Is.Not.Null);
            Assert.That(fileTs.File.Path, Is.EqualTo(".\\..\\..\\..\\WindEnergy.csv"));
            Assert.That(fileTs.File.Extension, Is.EqualTo("CSV"));
        }

        [Test]
        [TestCase("")]
        [TestCase(";")]
        [TestCase(";")]
        [TestCase("; ;   ;")]
        public virtual void Parse_ValidEnd_DoesNotThrow(string ending)
        {
            var text = "CREATE OR REPLACE TIMESERIES WindEnergy(Instant TIMESTAMP, Forecasted MEASUREMENT)";
            text += ending;
            Assert.DoesNotThrow(() => DmlParser.Timeseries.Parse(text));
        }

        [Test]
        [TestCase("END")]
        [TestCase(";END")]
        [TestCase("END;")]
        [TestCase(";END;")]
        public virtual void Parse_InvalidEnd_Throw(string ending)
        {
            var text = "CREATE OR REPLACE TIMESERIES WindEnergy(Instant TIMESTAMP, Forecasted MEASUREMENT)";
            text += ending;
            var ex = Assert.Throws<ParseException>(() => DmlParser.Timeseries.Parse(text));
            Assert.That(ex.Message, Does.Contain("unexpected 'E'"));
        }
    }
}
