using DubUrl.Mapping;
using DubUrl.Querying.Dialects;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Mounting;
using Tseesecake.Parsing.Dml;
using Tseesecake.Parsing.Query;
using Tseesecake.Querying.Aggregations;
using Tseesecake.Querying.Expressions;
using Tseesecake.Querying.Filters;
using Tseesecake.Querying.Projections;

namespace Tseesecake.Testing.Parsing.Query
{
    internal class QueryParserTest
    {
        [Test]
        public virtual void Parse_Columns_Valid()
        {
            var text = "SELECT Instant, WindPark, Forecasted FROM WindEnergy";
            var query = QueryParser.Query.Parse(text);
            Assert.That(query, Is.Not.Null);
            Assert.That(query.Timeseries.Name, Is.EqualTo("WindEnergy"));
            Assert.That(query.Projections, Has.Length.EqualTo(3));
            foreach (var projection in query.Projections)
                Assert.That(projection, Is.TypeOf<ColumnProjection>());
            var columnNames = query.Projections.Cast<ColumnProjection>().Select(x => x.Alias);
            Assert.That(columnNames, Does.Contain("Instant"));
            Assert.That(columnNames, Does.Contain("WindPark"));
            Assert.That(columnNames, Does.Contain("Forecasted"));
        }

        [Test]
        public virtual void Parse_Expression_Valid()
        {
            var text = "SELECT `LOWER(WindPark)` AS ExpressionValue, 10.0 AS ConstantValue FROM WindEnergy";
            var query = QueryParser.Query.Parse(text);
            Assert.That(query, Is.Not.Null);
            Assert.That(query.Timeseries.Name, Is.EqualTo("WindEnergy"));
            Assert.That(query.Projections, Has.Length.EqualTo(2));
            foreach (var projection in query.Projections)
                Assert.That(projection, Is.TypeOf<ExpressionProjection>());
            var columnNames = query.Projections.Cast<ExpressionProjection>().Select(x => x.Alias);
            Assert.That(columnNames, Does.Contain("ExpressionValue"));
            Assert.That(columnNames, Does.Contain("ConstantValue"));

            var expressions = query.Projections.Cast<ExpressionProjection>().Select(x => x.Expression);
            Assert.That(expressions.ElementAt(0), Is.TypeOf<LiteralExpression>());
            Assert.That(expressions.ElementAt(1), Is.TypeOf<ConstantExpression>());
        }

        [Test]
        public virtual void Parse_Aggregation_Valid()
        {
            var text = "SELECT MAX(Forecasted) AS MaxValue, MIN(Forecasted) AS MinValue FROM WindEnergy";
            var query = QueryParser.Query.Parse(text);
            Assert.That(query, Is.Not.Null);
            Assert.That(query.Timeseries.Name, Is.EqualTo("WindEnergy"));
            Assert.That(query.Projections, Has.Length.EqualTo(2));
            foreach (var projection in query.Projections)
                Assert.That(projection, Is.TypeOf<AggregationProjection>());
            var columnNames = query.Projections.Cast<AggregationProjection>().Select(x => x.Alias);
            Assert.That(columnNames, Does.Contain("MaxValue"));
            Assert.That(columnNames, Does.Contain("MinValue"));

            var aggregations = query.Projections.Cast<AggregationProjection>().Select(x => x.Aggregation);
            Assert.That(aggregations.ElementAt(0), Is.TypeOf<MaxAggregation>());
            Assert.That(aggregations.ElementAt(1), Is.TypeOf<MinAggregation>());

            foreach (var aggregation in aggregations)
            {
                Assert.That(aggregation.Expression, Is.TypeOf<ColumnExpression>());
                Assert.That(((ColumnExpression)aggregation.Expression).Column.Name, Is.EqualTo("Forecasted"));
            }
        }

        [Test]
        public virtual void Parse_TemporizerFilters_Valid()
        {
            var text = "SELECT Instant, WindPark, Forecasted FROM WindEnergy WHERE Instant AFTER TIMESTAMP '2023-01-01' AND Instant SINCE INTERVAL '100.00:00:00'";
            var query = QueryParser.Query.Parse(text);
            Assert.That(query, Is.Not.Null);
            Assert.That(query.Timeseries.Name, Is.EqualTo("WindEnergy"));
            Assert.That(query.Filters, Has.Length.EqualTo(2));
            foreach (var filter in query.Filters)
                Assert.That(filter, Is.AssignableTo<Temporizer>());
        }

        [Test]
        public virtual void Parse_DicerAndTemporizerFilters_Valid()
        {
            var text = "SELECT Instant, WindPark, Forecasted FROM WindEnergy WHERE Instant AFTER TIMESTAMP '2023-01-01' AND Producer IS 'Future Energy'";
            var query = QueryParser.Query.Parse(text);
            Assert.That(query, Is.Not.Null);
            Assert.That(query.Timeseries.Name, Is.EqualTo("WindEnergy"));
            Assert.That(query.Filters, Has.Length.EqualTo(2));
            Assert.That(query.Filters.ElementAt(0), Is.AssignableTo<Temporizer>());
            Assert.That(query.Filters.ElementAt(1), Is.AssignableTo<Dicer>());
        }

        [Test]
        public virtual void Parse_DicerAndTemporizerAndCullerFilters_Valid()
        {
            var text = "SELECT Instant, WindPark, Forecasted FROM WindEnergy WHERE Instant BEFORE TIMESTAMP '2023-01-01' AND Producer IS NOT 'Future Energy' AND NOT(Forecasted>10)";
            var query = QueryParser.Query.Parse(text);
            Assert.That(query, Is.Not.Null);
            Assert.That(query.Timeseries.Name, Is.EqualTo("WindEnergy"));
            Assert.That(query.Filters, Has.Length.EqualTo(3));
            Assert.That(query.Filters.ElementAt(0), Is.AssignableTo<Temporizer>());
            Assert.That(query.Filters.ElementAt(1), Is.AssignableTo<Dicer>());
            Assert.That(query.Filters.ElementAt(2), Is.AssignableTo<Sifter>());
        }

        [Test]
        public virtual void Parse_OrderBy_Valid()
        {
            var text = "SELECT Instant, WindPark, Forecasted FROM WindEnergy ORDER BY Instant, Forecasted";
            var query = QueryParser.Query.Parse(text);
            Assert.That(query, Is.Not.Null);
            Assert.That(query.Timeseries.Name, Is.EqualTo("WindEnergy"));
            Assert.That(query.Orders, Has.Length.EqualTo(2));
        }
    }
}
