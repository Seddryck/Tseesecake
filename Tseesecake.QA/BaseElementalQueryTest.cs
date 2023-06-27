using DubUrl.Extensions.DependencyInjection;
using DubUrl.Mapping;
using DubUrl.Querying.Dialects;
using DubUrl.Registering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tseesecake.Engine;
using Tseesecake.Testing.Engine;

namespace Tseesecake.QA
{
    public abstract class BaseElementalQueryTest
    {
        [OneTimeSetUp]
        public void SetupFixture()
        {
            var options = new DubUrlServiceOptions();
            Provider = new ServiceCollection()
                .AddSingleton(EmptyDubUrlConfiguration)
                .AddDubUrl(options)
                .AddTransient(provider => ActivatorUtilities.CreateInstance<QueryEngine>(provider
                    , new[] { ConnectionString }))
                .BuildServiceProvider();

            new ProviderFactoriesRegistrator().Register();
        }

        protected ServiceProvider Provider { get; set; }
        public abstract string ConnectionString { get; }

        protected static IConfiguration EmptyDubUrlConfiguration
        {
            get
            {
                var builder = new ConfigurationBuilder().AddInMemoryCollection();
                return builder.Build();
            }
        }

        [Test]
        public virtual void Execute_ProjectionSingle_ValidStatement()
        {
            var engine = Provider.GetRequiredService<QueryEngine>();
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
            var engine = Provider.GetRequiredService<QueryEngine>();
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
            var engine = Provider.GetRequiredService<QueryEngine>();
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
            var engine = Provider.GetRequiredService<QueryEngine>();
            var reader = engine.ExecuteReader(SelectStatementDefinition.ProjectionAggregation);
            Assert.That(reader, Is.Not.Null);
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.FieldCount, Is.EqualTo(1));
            Assert.That(reader.GetName(0), Is.EqualTo("Maximum"));
            Assert.That(reader.GetDecimal(0), Is.EqualTo(30));
            Assert.That(reader.Read(), Is.False);
        }

        [Test]
        public virtual void Execute_FilterSingle_ValidStatement()
        {
            var engine = Provider.GetRequiredService<QueryEngine>();
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
            var engine = Provider.GetRequiredService<QueryEngine>();
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
            var engine = Provider.GetRequiredService<QueryEngine>();
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
            var engine = Provider.GetRequiredService<QueryEngine>();
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
            var engine = Provider.GetRequiredService<QueryEngine>();
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
            var engine = Provider.GetRequiredService<QueryEngine>();
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
            var engine = Provider.GetRequiredService<QueryEngine>();
            var reader = engine.ExecuteReader(SelectStatementDefinition.SlicerAndGroupFilter);
            Assert.That(reader, Is.Not.Null);
            var rowCount = 0;
            while (reader.Read())
                rowCount += 1;
            Assert.That(rowCount, Is.EqualTo(2));
        }

        [Test]
        public virtual void Execute_LimitOffset_ValidStatement()
        {
            var engine = Provider.GetRequiredService<QueryEngine>();
            var reader = engine.ExecuteReader(SelectStatementDefinition.LimitOffset);
            Assert.That(reader, Is.Not.Null);
            var rowCount = 0;
            while (reader.Read())
                rowCount += 1;
            Assert.That(rowCount, Is.EqualTo(20));
        }
    }
}
