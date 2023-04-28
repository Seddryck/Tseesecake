using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Engine;
using Tseesecake.Modeling;
using Tseesecake.Querying;

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
                    , new DataPoint("value")
                );

            var select = new SelectStatement(ts
                , new[] { 
                    new ColumnProjection(new Timestamp("value"))
                });

            var response = new Translator().Execute(select);
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.EqualTo("SELECT\r\n\tvalue\r\nFROM\r\n\tWindForecast"));
        }

        [Test]
        public void Execute_MultipleProjection_ValidStatement()
        {
            var ts = new Timeseries(
                    "WindForecast"
                    , new Timestamp("instant")
                    , new DataPoint("value")
                );

            var select = new SelectStatement(ts
                , new[] {
                    new ColumnProjection(new Timestamp("instant"))
                    , new ColumnProjection(new Timestamp("value"))
                });

            var response = new Translator().Execute(select);
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.EqualTo("SELECT\r\n\tinstant\r\n\t, value\r\nFROM\r\n\tWindForecast"));
        }

        [Test]
        public void Execute_Expression_ValidStatement()
        {
            var ts = new Timeseries(
                    "WindForecast"
                    , new Timestamp("instant")
                    , new DataPoint("value")
                );

            var select = new SelectStatement(ts
                , new[] {
                    new ExpressionProjection("MAX(value)", "maximum")
                });

            var response = new Translator().Execute(select);
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.EqualTo("SELECT\r\n\tMAX(value) AS maximum\r\nFROM\r\n\tWindForecast"));
        }
    }
}
