using DubUrl.Mapping;
using DubUrl.Querying.Dialects;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements.Aggregations;
using Tseesecake.Modeling.Statements.Arguments;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Filters;
using Tseesecake.Modeling.Statements.Projections;
using Tseesecake.Modeling.Statements.Slicers;
using Tseesecake.Parsing.Select;

namespace Tseesecake.Testing.Parsing.Select
{
    internal class SelectStatementParserTest
    {
        [Test]
        public virtual void Parse_Columns_Valid()
        {
            var text = "SELECT Instant, WindPark, Forecasted FROM WindEnergy";
            var query = SelectStatementParser.Query.Parse(text);
            Assert.That(query, Is.Not.Null);
            Assert.That(query.Timeseries.Name, Is.EqualTo("WindEnergy"));
            Assert.That(query.Projections, Has.Count.EqualTo(3));
            var columnNames = query.Projections.Select(x => x.Alias);
            Assert.That(columnNames, Does.Contain("Instant"));
            Assert.That(columnNames, Does.Contain("WindPark"));
            Assert.That(columnNames, Does.Contain("Forecasted"));
        }

        [Test]
        public virtual void Parse_LiteralExpression_Valid()
        {
            var text = "SELECT `LOWER(WindPark)` AS ExpressionValue FROM WindEnergy";
            var query = SelectStatementParser.Query.Parse(text);
            Assert.That(query, Is.Not.Null);
            Assert.That(query.Timeseries.Name, Is.EqualTo("WindEnergy"));
            Assert.That(query.Projections, Has.Count.EqualTo(1));
            Assert.That(query.Projections[0].Alias, Is.EqualTo("ExpressionValue"));
            Assert.That(query.Projections[0].Expression, Is.TypeOf<LiteralExpression>());
        }

        [Test]
        public virtual void Parse_ConstantExpression_Valid()
        {
            var text = "SELECT 10.0 AS ConstantValue FROM WindEnergy";
            var query = SelectStatementParser.Query.Parse(text);
            Assert.That(query, Is.Not.Null);
            Assert.That(query.Timeseries.Name, Is.EqualTo("WindEnergy"));
            Assert.That(query.Projections, Has.Count.EqualTo(1));
            Assert.That(query.Projections[0].Alias, Is.EqualTo("ConstantValue"));
            Assert.That(query.Projections[0].Expression, Is.TypeOf<ConstantExpression>());
        }

        [Test]
        public virtual void Parse_Aggregation_Valid()
        {
            var text = "SELECT MAX(Forecasted) AS MaxValue, MIN(Forecasted) AS MinValue FROM WindEnergy";
            var query = SelectStatementParser.Query.Parse(text);
            Assert.That(query, Is.Not.Null);
            Assert.That(query.Timeseries.Name, Is.EqualTo("WindEnergy"));
            Assert.That(query.Projections, Has.Count.EqualTo(2));
            var columnNames = query.Projections.Select(x => x.Alias);
            Assert.That(columnNames, Does.Contain("MaxValue"));
            Assert.That(columnNames, Does.Contain("MinValue"));

            var aggregations = query.Projections.Select(x => x.Expression);
            foreach (var aggregation in aggregations)
                Assert.That(aggregation, Is.TypeOf<AggregationExpression>());
            Assert.That(((AggregationExpression)aggregations.ElementAt(0)).Aggregation, Is.TypeOf<MaxAggregation>());
            Assert.That(((AggregationExpression)aggregations.ElementAt(1)).Aggregation, Is.TypeOf<MinAggregation>());

            foreach (var aggregation in aggregations.Cast<AggregationExpression>())
            {
                Assert.That(aggregation.Aggregation.Expression, Is.TypeOf<ColumnReference>());
                Assert.That(((ColumnReference)aggregation.Aggregation.Expression).Name, Is.EqualTo("Forecasted"));
            }
        }

        [Test]
        public virtual void Parse_TemporizerFilters_Valid()
        {
            var text = "SELECT Instant, WindPark, Forecasted FROM WindEnergy WHERE Instant AFTER TIMESTAMP '2023-01-01' AND Instant SINCE INTERVAL '100.00:00:00'";
            var query = SelectStatementParser.Query.Parse(text);
            Assert.That(query, Is.Not.Null);
            Assert.That(query.Timeseries.Name, Is.EqualTo("WindEnergy"));
            Assert.That(query.Filters, Has.Count.EqualTo(2));
            foreach (var filter in query.Filters)
                Assert.That(filter, Is.AssignableTo<Temporizer>());
        }

        [Test]
        public virtual void Parse_DicerAndTemporizerFilters_Valid()
        {
            var text = "SELECT Instant, WindPark, Forecasted FROM WindEnergy WHERE Instant AFTER TIMESTAMP '2023-01-01' AND Producer IS 'Future Energy'";
            var query = SelectStatementParser.Query.Parse(text);
            Assert.That(query, Is.Not.Null);
            Assert.That(query.Timeseries.Name, Is.EqualTo("WindEnergy"));
            Assert.That(query.Filters, Has.Count.EqualTo(2));
            Assert.That(query.Filters.ElementAt(0), Is.AssignableTo<Temporizer>());
            Assert.That(query.Filters.ElementAt(1), Is.AssignableTo<Dicer>());
        }

        [Test]
        public virtual void Parse_DicerAndTemporizerAndCullerFilters_Valid()
        {
            var text = "SELECT Instant, WindPark, Forecasted FROM WindEnergy WHERE Instant BEFORE TIMESTAMP '2023-01-01' AND Producer IS NOT 'Future Energy' AND NOT(Forecasted>10)";
            var query = SelectStatementParser.Query.Parse(text);
            Assert.That(query, Is.Not.Null);
            Assert.That(query.Timeseries.Name, Is.EqualTo("WindEnergy"));
            Assert.That(query.Filters, Has.Count.EqualTo(3));
            Assert.That(query.Filters.ElementAt(0), Is.AssignableTo<Temporizer>());
            Assert.That(query.Filters.ElementAt(1), Is.AssignableTo<Dicer>());
            Assert.That(query.Filters.ElementAt(2), Is.AssignableTo<Sifter>());
        }

        [Test]
        public virtual void Parse_OrderBy_Valid()
        {
            var text = "SELECT Instant, WindPark, Forecasted FROM WindEnergy ORDER BY Instant, Forecasted";
            var query = SelectStatementParser.Query.Parse(text);
            Assert.That(query, Is.Not.Null);
            Assert.That(query.Timeseries.Name, Is.EqualTo("WindEnergy"));
            Assert.That(query.Orders, Has.Count.EqualTo(2));
        }

        [Test]
        public virtual void Parse_Restriction_Valid()
        {
            var text = "SELECT Instant, WindPark, Forecasted FROM WindEnergy LIMIT 20 OFFSET 30";
            var query = SelectStatementParser.Query.Parse(text);
            Assert.That(query, Is.Not.Null);
            Assert.That(query.Timeseries.Name, Is.EqualTo("WindEnergy"));
            Assert.That(query.Restriction, Is.Not.Null);
        }

        [Test]
        public virtual void Parse_GroupBy_Valid()
        {
            var text = "SELECT WindPark, Producer, MAX(Forecasted) AS MaxForecasted FROM WindEnergy GROUP BY WindPark, Producer";
            var query = SelectStatementParser.Query.Parse(text);
            Assert.That(query, Is.Not.Null);
            Assert.That(query.Timeseries.Name, Is.EqualTo("WindEnergy"));
            Assert.That(query.Slicers, Is.Not.Null);
            Assert.That(query.Slicers, Has.Count.EqualTo(2));
        }

        [Test]
        public virtual void Parse_BucketBy_Valid()
        {
            var text = "SELECT MAX(Forecasted) AS MaxForecasted FROM WindEnergy BUCKET Instant BY Month";
            var query = SelectStatementParser.Query.Parse(text);

            Assert.That(query, Is.Not.Null);
            Assert.That(query.Timeseries.Name, Is.EqualTo("WindEnergy"));
            Assert.That(query.Slicers, Is.Not.Null);
            Assert.That(query.Slicers, Has.Count.EqualTo(1));
            Assert.That(query.Slicers.ElementAt(0), Is.TypeOf<TruncationTemporalSlicer>());

            var slicer = (TruncationTemporalSlicer)query.Slicers.ElementAt(0);
            Assert.That(slicer.Timestamp.Name, Is.EqualTo("Instant"));
            Assert.That(slicer.Truncation, Is.EqualTo(TruncationTemporal.Month));
        }

        [Test]
        public virtual void Parse_BucketByAnonymous_Valid()
        {
            var text = "SELECT MAX(Forecasted) AS MaxForecasted FROM WindEnergy BUCKET BY Month";
            var query = SelectStatementParser.Query.Parse(text);

            Assert.That(query, Is.Not.Null);
            Assert.That(query.Timeseries.Name, Is.EqualTo("WindEnergy"));
            Assert.That(query.Slicers, Is.Not.Null);
            Assert.That(query.Slicers, Has.Count.EqualTo(1));
            Assert.That(query.Slicers.ElementAt(0), Is.TypeOf<TruncationTemporalSlicer>());

            var slicer = (TruncationTemporalSlicer)query.Slicers.ElementAt(0);
            Assert.That(slicer.Timestamp, Is.TypeOf<AnonymousTimestamp>());
            Assert.That(slicer.Truncation, Is.EqualTo(TruncationTemporal.Month));
        }

        [Test]
        public virtual void Parse_Expressions_Valid()
        {
            var text = "WITH MEASUREMENT Accuracy AS (Forecasted - Produced) SELECT Accuracy FROM WindEnergy";
            var query = SelectStatementParser.Query.Parse(text);
            Assert.That(query, Is.Not.Null);
            Assert.That(query.Timeseries.Name, Is.EqualTo("WindEnergy"));
            Assert.That(query.Projections, Has.Count.EqualTo(1));
            var columnName = query.Projections.Select(x => x.Alias).First();
            Assert.That(columnName, Is.EqualTo("Accuracy"));

            Assert.That(query.VirtualMeasurements, Has.Count.EqualTo(1));
            foreach (var expr in query.VirtualMeasurements)
                Assert.That(expr, Is.TypeOf<VirtualMeasurement>());
            var expression = query.VirtualMeasurements.First();
            Assert.That(expression.Name, Is.EqualTo("Accuracy"));
            Assert.That(expression.Expression, Is.Not.Null);
        }
    }
}
