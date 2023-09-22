using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements;

namespace Tseesecake.Testing.Modeling
{
    public class MetaEngineTest
    {
        public static Timeseries WindEnergy
            => new(
                    "WindEnergy"
                    , new Timestamp("Instant")
                    , new[] { new Measurement("Forecasted"), new Measurement("Produced") }
                    , new[] { new Facet("WindPark"), new Facet("Producer") }
                );

        public static Timeseries SolarEnergy
            => new(
                    "SolarEnergy"
                    , new Timestamp("Instant")
                    , new[] { new Measurement("Forecasted"), new Measurement("Produced") }
                    , new[] { new Facet("Region"), new Facet("Producer"), new Facet("ProductionType") }
                );

        [Test]
        public void Execute_ShowAllTimeseries_AllOfThem()
        {
            var statement = new ShowAllTimeseries();
            var engine = new CatalogEngine(new[] { WindEnergy, SolarEnergy });
            var reader = engine.ExecuteReader(statement);

            Assert.That(reader, Is.Not.Null );
            Assert.That(reader.FieldCount, Is.EqualTo(1));
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.GetString(0), Is.EqualTo(WindEnergy.Name));
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.GetString(0), Is.EqualTo(SolarEnergy.Name));
            Assert.That(reader.Read(), Is.False);
        }

        [Test]
        public void Execute_ShowFieldsTimeseries_AllOfThem()
        {
            var statement = new ShowFieldsTimeseries(WindEnergy.Name);
            var engine = new CatalogEngine(new[] { WindEnergy, SolarEnergy });
            var reader = engine.ExecuteReader(statement);

            Assert.That(reader, Is.Not.Null);
            Assert.That(reader.FieldCount, Is.EqualTo(5));
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.GetString(0), Is.EqualTo(WindEnergy.Name));
            Assert.That(reader.GetString(1), Is.EqualTo(WindEnergy.Timestamp.Name));
            Assert.That(reader.GetInt32(2), Is.EqualTo(1));
            Assert.That(reader.GetString(3), Is.EqualTo("Timestamp"));
            Assert.That(reader.GetString(4), Is.EqualTo("DateTime"));
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.GetString(0), Is.EqualTo(WindEnergy.Name));
            Assert.That(reader.GetString(1), Is.EqualTo(WindEnergy.Facets[0].Name));
            Assert.That(reader.GetInt32(2), Is.EqualTo(2));
            Assert.That(reader.GetString(3), Is.EqualTo("Facet"));
            Assert.That(reader.GetString(4), Is.EqualTo("String"));
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.GetString(0), Is.EqualTo(WindEnergy.Name));
            Assert.That(reader.GetString(1), Is.EqualTo(WindEnergy.Facets[1].Name));
            Assert.That(reader.GetInt32(2), Is.EqualTo(3));
            Assert.That(reader.GetString(3), Is.EqualTo("Facet"));
            Assert.That(reader.GetString(4), Is.EqualTo("String"));
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.GetString(0), Is.EqualTo(WindEnergy.Name));
            Assert.That(reader.GetString(1), Is.EqualTo(WindEnergy.Measurements[0].Name));
            Assert.That(reader.GetInt32(2), Is.EqualTo(4));
            Assert.That(reader.GetString(3), Is.EqualTo("Measurement"));
            Assert.That(reader.GetString(4), Is.EqualTo("Decimal"));
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.GetString(0), Is.EqualTo(WindEnergy.Name));
            Assert.That(reader.GetString(1), Is.EqualTo(WindEnergy.Measurements[1].Name));
            Assert.That(reader.GetInt32(2), Is.EqualTo(5));
            Assert.That(reader.GetString(3), Is.EqualTo("Measurement"));
            Assert.That(reader.GetString(4), Is.EqualTo("Decimal"));
            Assert.That(reader.Read(), Is.False);
        }
    }
}