using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Parsing.Query;

namespace Tseesecake.Testing.Parsing.Query
{
    public class VirtualMeasurementParserTest
    {
        [Test]
        [TestCase("WITH MEASUREMENT Accuracy AS (Forecasted - Produced)", "Accuracy", typeof(BinaryExpression))]
        [TestCase("WITH MEASUREMENT \"Forecasted (GW)\" AS (Forecasted / 1000)", "Forecasted (GW)", typeof(BinaryExpression))]
        public void Parse_OrderByColumns_CorrectValue(string text, string name, Type expressionType)
        {
            var measurement = VirtualMeasurementParser.VirtualMeasurement.Parse(text);
            Assert.That(measurement, Is.Not.Null);
            Assert.That(measurement.Name, Is.EqualTo(name));
            Assert.That(measurement.Expression, Is.AssignableTo(expressionType));
        }
    }
}
