using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Engine;
using Tseesecake.Modeling;
using Tseesecake.Querying;
using Tseesecake.Querying.Filters;
using Tseesecake.Querying.Slicers;

namespace Tseesecake.Testing.Engine
{
    public class TranslatorTest
    {
        [Test]
        public void Execute_SingleProjection_ValidStatement()
        {
            var ts = new Timeseries(
                    "WindForecast"
                    , new Timestamp("instant")
                    , new Measurement("value")
                );

            var select = new SelectStatement(ts
                , new[] { 
                    new ColumnProjection(new Timestamp("value"))
                });

            var response = new Translator().Execute(select);
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.EqualTo("SELECT\r\n\tvalue\r\nFROM\r\n\tWindForecast\r\n"));
        }

        [Test]
        public void Execute_MultipleProjection_ValidStatement()
        {
            var ts = new Timeseries(
                    "WindForecast"
                    , new Timestamp("instant")
                    , new Measurement("value")
                );

            var select = new SelectStatement(ts
                , new[] {
                    new ColumnProjection(new Timestamp("instant"))
                    , new ColumnProjection(new Timestamp("value"))
                });

            var response = new Translator().Execute(select);
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.EqualTo("SELECT\r\n\tinstant\r\n\t, value\r\nFROM\r\n\tWindForecast\r\n"));
        }

        [Test]
        public void Execute_Expression_ValidStatement()
        {
            var ts = new Timeseries(
                    "WindForecast"
                    , new Timestamp("instant")
                    , new Measurement("value")
                );

            var select = new SelectStatement(ts
                , new[] {
                    new ExpressionProjection("MAX(value)", "maximum")
                });

            var response = new Translator().Execute(select);
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.EqualTo("SELECT\r\n\tMAX(value) AS maximum\r\nFROM\r\n\tWindForecast\r\n"));
        }

        [Test]
        public void Execute_SingleDicer_ValidStatement()
        {
            var ts = new Timeseries(
                    "WindForecast"
                    , new Timestamp("instant")
                    , new Measurement("value")
                    , new[] { new Facet("Location") }
                );

            var select = new SelectStatement(ts
                , new[] {
                    new ColumnProjection(new Timestamp("value"))
                }
                , new[] {
                    new EqualDicer(new Facet("Location"), "Brussels")
                });

            var response = new Translator().Execute(select);
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.EqualTo("SELECT\r\n\tvalue\r\nFROM\r\n\tWindForecast\r\nWHERE\r\n\tLocation = 'Brussels'\r\n"));
        }


        [Test]
        public void Execute_MultipleDicers_ValidStatement()
        {
            var ts = new Timeseries(
                    "WindForecast"
                    , new Timestamp("instant")
                    , new Measurement("value")
                    , new[] { new Facet("Location"), new Facet("Action"), new Facet("Customer") }
                );

            var select = new SelectStatement(ts
                , new[] {
                    new ColumnProjection(new Timestamp("value"))
                }
                , new IFilter[] {
                    new EqualDicer(new Facet("Location"), "Brussels")
                    , new DifferentDicer(new Facet("Action"), "Phone call")
                    , new InDicer(new Facet("Customer"), new[] {"Foo", "Bar" })
                });

            var response = new Translator().Execute(select);
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.EqualTo("SELECT\r\n\tvalue\r\nFROM\r\n\tWindForecast\r\nWHERE\r\n\tLocation = 'Brussels'\r\n\tAND Action != 'Phone call'\r\n\tAND Customer IN ('Foo', 'Bar')\r\n"));
        }

        [Test]
        public void Execute_Culler_ValidStatement()
        {
            var ts = new Timeseries(
                    "WindForecast"
                    , new Timestamp("instant")
                    , new Measurement("value")
                    , new[] { new Facet("Location"), new Facet("Action"), new Facet("Customer") }
                );

            var select = new SelectStatement(ts
                , new[] {
                    new ColumnProjection(new Timestamp("value"))
                }
                , new IFilter[] {
                    new Culler(new Measurement("value"), "< 100")
                });

            var response = new Translator().Execute(select);
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.EqualTo("SELECT\r\n\tvalue\r\nFROM\r\n\tWindForecast\r\nWHERE\r\n\tNOT(value < 100)\r\n"));
        }

        [Test]
        public void Execute_Temporizer_ValidStatement()
        {
            var ts = new Timeseries(
                    "WindForecast"
                    , new Timestamp("instant")
                    , new Measurement("value")
                    , new[] { new Facet("Location"), new Facet("Action"), new Facet("Customer") }
                );

            var select = new SelectStatement(ts
                , new[] {
                    new ColumnProjection(new Timestamp("value"))
                }
                , new IFilter[] {
                    new SinceTemporizer(new Timestamp("instant"), new TimeSpan(4, 30, 0))
                });

            var response = new Translator().Execute(select);
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.EqualTo("SELECT\r\n\tvalue\r\nFROM\r\n\tWindForecast\r\nWHERE\r\n\tage(instant) < 4 HOUR 30 MINUTE\r\n"));
        }

        [Test]
        public void Execute_SingleSlicer_ValidStatement()
        {
            var ts = new Timeseries(
                    "WindForecast"
                    , new Timestamp("instant")
                    , new Measurement("value")
                    , new[] { new Facet("Location"), new Facet("Action"), new Facet("Customer") }
                );

            var select = new SelectStatement(ts
                , new[] {
                    new ExpressionProjection("MAX(value)", "maximum")
                }
                , null
                , new ISlicer[] {
                    new FacetSlicer(new Facet("Location"))
                });

            var response = new Translator().Execute(select);
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.EqualTo("SELECT\r\n\tMAX(value) AS maximum\r\nFROM\r\n\tWindForecast\r\nGROUP BY\r\n\tLocation\r\n"));
        }

        [Test]
        public void Execute_ManySlicers_ValidStatement()
        {
            var ts = new Timeseries(
                    "WindForecast"
                    , new Timestamp("instant")
                    , new Measurement("value")
                    , new[] { new Facet("location"), new Facet("action"), new Facet("customer") }
                );

            var select = new SelectStatement(ts
                , new[] {
                    new ExpressionProjection("MAX(value)", "maximum")
                }
                , null
                , new ISlicer[] {
                    new FacetSlicer(new Facet("location"))
                    , new PartTemporalSlicer(new Timestamp("instant"), "weekday")
                });

            var response = new Translator().Execute(select);
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.EqualTo("SELECT\r\n\tMAX(value) AS maximum\r\nFROM\r\n\tWindForecast\r\nGROUP BY\r\n\tlocation\r\n\t, date_part('weekday', instant)\r\n"));
        }
    }
}
