using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tseesecake.Modeling;
using Tseesecake.Testing.Engine;
using Tseesecake.Testing.Engine.Statements;

namespace Tseesecake.QA
{
    public abstract class BaseSelectCommandTest : BaseQATest
    {
        [OneTimeSetUp]
        public void SetupFixture()
            => SetupEngine(new[] { typeof(SelectEngine) });

        [Test]
        public virtual void Execute_ProjectionSingle_ValidStatement()
        {
            var engine = Provider.GetRequiredService<SelectEngine>();
            var reader = engine.ExecuteReader(SelectStatementDefinition.ProjectionSingle);
            Assert.That(reader, Is.Not.Null);
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.FieldCount, Is.EqualTo(1));
            Assert.That(reader.GetName(0), Is.EqualTo("Produced"));
            Assert.That(reader.GetDecimal(0), Is.GreaterThanOrEqualTo(0));
        }

        [Test]
        public virtual void Execute_ProjectionMultiple_ValidStatement()
        {
            var engine = Provider.GetRequiredService<SelectEngine>();
            var reader = engine.ExecuteReader(SelectStatementDefinition.ProjectionMultiple);
            Assert.That(reader, Is.Not.Null);
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.FieldCount, Is.EqualTo(2));
            Assert.That(reader.GetName(0), Is.EqualTo("Instant"));
            Assert.That(reader.GetName(1), Is.EqualTo("Produced"));
            Assert.That(reader.GetDateTime(0), Is.GreaterThanOrEqualTo(new DateTime(2022, 12, 28)));
            Assert.That(reader.GetDecimal(1), Is.GreaterThanOrEqualTo(0));
        }

        [Test]
        public virtual void Execute_ProjectionExpression_ValidStatement()
        {
            var engine = Provider.GetRequiredService<SelectEngine>();
            var reader = engine.ExecuteReader(SelectStatementDefinition.ProjectionExpression);
            Assert.That(reader, Is.Not.Null);
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.FieldCount, Is.EqualTo(1));
            Assert.That(reader.GetName(0), Is.EqualTo("LowerWindPark"));
            Assert.That(reader.GetString(0), Is.EqualTo(reader.GetString(0).ToLower()));
        }

        [Test]
        public virtual void Execute_ProjectionAggregation_ValidStatement()
        {
            var engine = Provider.GetRequiredService<SelectEngine>();
            var reader = engine.ExecuteReader(SelectStatementDefinition.ProjectionAggregation);
            Assert.That(reader, Is.Not.Null);
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.FieldCount, Is.EqualTo(1));
            Assert.That(reader.GetName(0), Is.EqualTo("Maximum"));
            Assert.That(reader.GetDecimal(0), Is.EqualTo(30));
            Assert.That(reader.Read(), Is.False);
        }

        [Test]
        public virtual void Execute_ProjectionAggregationFilter_ValidStatement()
        {
            var engine = Provider.GetRequiredService<SelectEngine>();
            var reader = engine.ExecuteReader(SelectStatementDefinition.ProjectionAggregationFilter);
            Assert.That(reader, Is.Not.Null);
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.FieldCount, Is.EqualTo(1));
            Assert.That(reader.GetName(0), Is.EqualTo("Average"));
            Assert.That(reader.GetDecimal(0), Is.GreaterThan(5));
            Assert.That(reader.GetDecimal(0), Is.LessThan(20));
            Assert.That(reader.Read(), Is.False);
        }

        [Test]
        public virtual void Execute_ProjectionWindow_ValidStatement()
        {
            var engine = Provider.GetRequiredService<SelectEngine>();
            var reader = engine.ExecuteReader(SelectStatementDefinition.ProjectionWindow);
            Assert.That(reader, Is.Not.Null);
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.FieldCount, Is.EqualTo(1));
            Assert.That(reader.GetName(0), Is.EqualTo("RowId"));
            Assert.That(reader.GetValue(0), Is.EqualTo(1));
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.GetValue(0), Is.EqualTo(2));
        }

        [Test]
        public virtual void Execute_ProjectionWindowOffset_ValidStatement()
        {
            var engine = Provider.GetRequiredService<SelectEngine>();
            var reader = engine.ExecuteReader(SelectStatementDefinition.ProjectionWindowOffset);
            Assert.That(reader, Is.Not.Null);
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.FieldCount, Is.EqualTo(1));
            Assert.That(reader.GetName(0), Is.EqualTo("FourHoursBefore"));
        }

        [Test]
        public virtual void Execute_ProjectionWindowOffsetExpression_ValidStatement()
        {
            var engine = Provider.GetRequiredService<SelectEngine>();
            var reader = engine.ExecuteReader(SelectStatementDefinition.ProjectionWindowOffsetExpression);
            Assert.That(reader, Is.Not.Null);
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.FieldCount, Is.EqualTo(1));
            Assert.That(reader.GetName(0), Is.EqualTo("FourHoursBefore"));
        }

        [Test]
        public virtual void Execute_ProjectionWindowFrame_ValidStatement()
        {
            var engine = Provider.GetRequiredService<SelectEngine>();
            var reader = engine.ExecuteReader(SelectStatementDefinition.ProjectionWindowFrame);
            Assert.That(reader, Is.Not.Null);
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.FieldCount, Is.EqualTo(1));
        }

        [Test]
        public virtual void Execute_FilterSingle_ValidStatement()
        {
            var engine = Provider.GetRequiredService<SelectEngine>();
            var reader = engine.ExecuteReader(SelectStatementDefinition.FilterSingle);
            Assert.That(reader, Is.Not.Null);
            var rowCount = 0;
            while (reader.Read())
                rowCount += 1;
            Assert.That(rowCount, Is.GreaterThan(15 * 24));
            Assert.That(rowCount, Is.LessThan(20 * 24));
        }

        [Test]
        public virtual void Execute_FilterMultiple_ValidStatement()
        {
            var engine = Provider.GetRequiredService<SelectEngine>();
            var reader = engine.ExecuteReader(SelectStatementDefinition.FilterMultiple);
            Assert.That(reader, Is.Not.Null);
            var rowCount = 0;
            while (reader.Read())
                rowCount += 1;
            Assert.That(rowCount, Is.GreaterThan(15 * 24));
            Assert.That(rowCount, Is.LessThan(20 * 24));
        }

        [Test]
        public virtual void Execute_FilterCuller_ValidStatement()
        {
            var engine = Provider.GetRequiredService<SelectEngine>();
            var reader = engine.ExecuteReader(SelectStatementDefinition.FilterCuller);
            Assert.That(reader, Is.Not.Null);
            var rowCount = 0;
            while (reader.Read())
                rowCount += 1;
            Assert.That(rowCount, Is.GreaterThan(1000));
            Assert.That(rowCount, Is.LessThan(5 * 18 * 24));
        }

        [Test]
        public virtual void Execute_FilterTemporizer_ValidStatement()
        {
            var engine = Provider.GetRequiredService<SelectEngine>();
            var reader = engine.ExecuteReader(SelectStatementDefinition.FilterTemporizer);
            Assert.That(reader, Is.Not.Null);
            var rowCount = 0;
            while (reader.Read())
                rowCount += 1;
            Assert.That(rowCount, Is.EqualTo(0));
        }

        [Test]
        public virtual void Execute_SlicerSingle_ValidStatement()
        {
            var engine = Provider.GetRequiredService<SelectEngine>();
            var reader = engine.ExecuteReader(SelectStatementDefinition.SlicerSingle);
            Assert.That(reader, Is.Not.Null);
            var rowCount = 0;
            while (reader.Read())
                rowCount += 1;
            Assert.That(rowCount, Is.EqualTo(5));
        }

        [Test]
        public virtual void Execute_SlicerMultiple_ValidStatement()
        {
            var engine = Provider.GetRequiredService<SelectEngine>();
            var reader = engine.ExecuteReader(SelectStatementDefinition.SlicerMultiple);
            Assert.That(reader, Is.Not.Null);
            var rowCount = 0;
            while (reader.Read())
                rowCount += 1;
            Assert.That(rowCount, Is.EqualTo(5 * 7));
        }

        [Test]
        public virtual void Execute_SlicerAndGroupFilter_ValidStatement()
        {
            var engine = Provider.GetRequiredService<SelectEngine>();
            var reader = engine.ExecuteReader(SelectStatementDefinition.SlicerAndGroupFilter);
            Assert.That(reader, Is.Not.Null);
            var rowCount = 0;
            while (reader.Read())
                rowCount += 1;
            Assert.That(rowCount, Is.EqualTo(2));
        }

        [Test]
        public virtual void Execute_NamedWindow_ValidStatement()
        {
            var engine = Provider.GetRequiredService<SelectEngine>();
            var reader = engine.ExecuteReader(SelectStatementDefinition.NamedWindow);
            Assert.That(reader, Is.Not.Null);
        }

        [Test]
        public virtual void Execute_Qualify_ValidStatement()
        {
            var engine = Provider.GetRequiredService<SelectEngine>();
            var reader = engine.ExecuteReader(SelectStatementDefinition.Qualify);
            Assert.That(reader, Is.Not.Null);
            var rowCount = 0;
            while (reader.Read())
                rowCount += 1;
            Assert.That(rowCount, Is.EqualTo(10));
        }

        [Test]
        public virtual void Execute_LimitOffset_ValidStatement()
        {
            var engine = Provider.GetRequiredService<SelectEngine>();
            var reader = engine.ExecuteReader(SelectStatementDefinition.LimitOffset);
            Assert.That(reader, Is.Not.Null);
            var rowCount = 0;
            while (reader.Read())
                rowCount += 1;
            Assert.That(rowCount, Is.EqualTo(20));
        }

        [Test]
        public virtual void ExecuteReader_BucketBy_ValidStatement()
        {
            var engine = Provider.GetRequiredService<SelectEngine>();
            var reader = engine.ExecuteReader(ResourcesReader.BucketBy);
            Assert.That(reader, Is.Not.Null);
            var rowCount = 0;
            while (reader.Read())
            {
                var weekId = reader.GetValue(0);
                Assert.That(weekId, Is.LessThanOrEqualTo(2).Or.GreaterThanOrEqualTo(52));
                rowCount += 1;
            }
            Assert.That(rowCount, Is.EqualTo(3));
        }

        [Test]
        public virtual void ExecuteReader_ImplicitGroupBy_ValidStatement()
        {
            var engine = Provider.GetRequiredService<SelectEngine>();
            var reader = engine.ExecuteReader(ResourcesReader.ImplicitGroupBy);
            Assert.That(reader, Is.Not.Null);
            Assert.That(reader.FieldCount, Is.EqualTo(3));
            var rowCount = 0;
            while (reader.Read())
            {
                Assert.That(reader.GetValue(0), Is.LessThanOrEqualTo(2).Or.GreaterThanOrEqualTo(52));
                Assert.That(reader.GetString(1), Is.AnyOf("Green Power Inc.", "Future Energy"));
                rowCount += 1;
            }
            Assert.That(rowCount, Is.EqualTo(6));
        }

        [Test]
        public virtual void ExecuteReader_VirtualMeasurement_ValidStatement()
        {
            var engine = Provider.GetRequiredService<SelectEngine>();
            var reader = engine.ExecuteReader(ResourcesReader.VirtualMeasurement);
            Assert.That(reader, Is.Not.Null);
            Assert.That(reader.FieldCount, Is.EqualTo(2));
            var rowCount = 0;
            object? previousValue = null;
            while (reader.Read())
            {
                if (previousValue is not null)
                    Assert.That(reader.GetValue(1), Is.LessThanOrEqualTo(previousValue));
                rowCount += 1;
                previousValue = reader.GetValue(1);
            }
            Assert.That(rowCount, Is.EqualTo(5)); 
        }
    }
}
