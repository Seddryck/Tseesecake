using Antlr4.StringTemplate;
using DubUrl.Mapping;
using DubUrl.Mapping.Database;
using DubUrl.Querying.Dialects;
using DubUrl.Registering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Engine;
using Tseesecake.Modeling;
using Tseesecake.Querying;
using Tseesecake.Querying.Filters;
using Tseesecake.Querying.Ordering;
using Tseesecake.Querying.Restrictions;
using Tseesecake.Querying.Slicers;

namespace Tseesecake.Testing.Engine
{
    public class BasicQueryTest
    {
        private IDialect Dialect { get; set; }
        private IConnectivity Connectivity { get; set; }

        [OneTimeSetUp]
        public void Initialize()
        {
            //Provider registration
            new ProviderFactoriesRegistrator().Register();

            //Scheme mapping
            var schemeMapperBuilder = new SchemeMapperBuilder();
            schemeMapperBuilder.Build();
            var mapper = schemeMapperBuilder.GetMapper("duckdb");
            (Dialect, Connectivity) = (mapper.GetDialect(), mapper.GetConnectivity());
        }

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

            var response = new BasicQuery(select).Read(Dialect, Connectivity);
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

            var response = new BasicQuery(select).Read(Dialect, Connectivity);
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

            var response = new BasicQuery(select).Read(Dialect, Connectivity);
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

            var response = new BasicQuery(select).Read(Dialect, Connectivity);
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

            var response = new BasicQuery(select).Read(Dialect, Connectivity);
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.EqualTo("SELECT\r\n\tvalue\r\nFROM\r\n\tWindForecast\r\nWHERE\r\n\tLocation = 'Brussels'\r\n\tAND Action <> 'Phone call'\r\n\tAND Customer IN ('Foo', 'Bar')\r\n"));
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
                    new Culler(new Measurement("value"), Expression.LessThan,  100)
                });

            var response = new BasicQuery(select).Read(Dialect, Connectivity);
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

            var response = new BasicQuery(select).Read(Dialect, Connectivity);
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.EqualTo("SELECT\r\n\tvalue\r\nFROM\r\n\tWindForecast\r\nWHERE\r\n\tage(instant) < INTERVAL '4 HOURS 30 MINUTES 0 SECONDS'\r\n"));
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

            var response = new BasicQuery(select).Read(Dialect, Connectivity);
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

            var response = new BasicQuery(select).Read(Dialect, Connectivity);
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.EqualTo("SELECT\r\n\tMAX(value) AS maximum\r\nFROM\r\n\tWindForecast\r\nGROUP BY\r\n\tlocation\r\n\t, date_part('weekday', instant)\r\n"));
        }


        [Test]
        public void Execute_SlicerAndGroupFilter_ValidStatement()
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
                }
                , new IFilter[] {
                    new Gatherer(new Measurement("maximum"), Expression.GreaterThanOrEqual, 120)
                });

            var response = new BasicQuery(select).Read(Dialect, Connectivity);
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.EqualTo("SELECT\r\n\tMAX(value) AS maximum\r\nFROM\r\n\tWindForecast\r\nGROUP BY\r\n\tlocation\r\nHAVING\r\n\tmaximum >= 120\r\n"));
        }

        [Test]
        public void Execute_LimitOffset_ValidStatement()
        {
            var ts = new Timeseries(
                    "WindForecast"
                    , new Timestamp("instant")
                    , new Measurement("value")
                    , new[] { new Facet("location"), new Facet("action"), new Facet("customer") }
                );

            var select = new SelectStatement(ts
                , new[] {
                    new ColumnProjection(new Measurement("value"))
                }
                , null
                , null
                , null
                , new IOrderBy[] {
                    new ColumnOrder(new Timestamp("instant"), Sorting.Descending, NullSorting.Last) }
                , new LimitOffsetRestriction(20,40));

            var response = new BasicQuery(select).Read(Dialect, Connectivity);
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.EqualTo("SELECT\r\n\tvalue\r\nFROM\r\n\tWindForecast\r\nORDER BY\r\n\tinstant DESC NULLS LAST\r\nLIMIT 20\r\nOFFSET 40\r\n"));
        }
    }
}
