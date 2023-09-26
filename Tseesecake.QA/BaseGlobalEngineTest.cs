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
using Tseesecake.Modeling;
using Tseesecake.Testing.Engine;

namespace Tseesecake.QA
{
    public abstract class BaseGlobalEngineTest : BaseQATest
    {
        [OneTimeSetUp]
        public void SetupFixture()
            => SetupEngine(new[] { typeof(GlobalEngine) });

        [Test]
        public virtual void Execute_MaxProduced_ValidResult()
        {
            var engine = Provider.GetRequiredService<GlobalEngine>();
            var reader = engine.ExecuteReader("SELECT max(Produced) AS AvgProduced FROM WindEnergy");
            Assert.That(reader, Is.Not.Null);
            Assert.That(reader.FieldCount, Is.EqualTo(1));
            Assert.That(reader.GetName(0), Is.EqualTo("AvgProduced"));
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.GetDecimal(0), Is.EqualTo(30m));
        }

        [Test]
        public virtual void Execute_WithArrangers_ValidStatement()
        {
            var engine = Provider.GetRequiredService<GlobalEngine>();
            var reader = engine.ExecuteReader("SELECT WindPark, avg(Produced) AS AvgProduced FROM WindEnergy");
            Assert.That(reader, Is.Not.Null);
            Assert.That(reader.FieldCount, Is.EqualTo(2));
            Assert.That(reader.GetName(0), Is.EqualTo("WindPark"));
            Assert.That(reader.GetName(1), Is.EqualTo("AvgProduced"));
            for (int i = 0; i < 5; i++)
            {
                Assert.That(reader.Read(), Is.True);
                Assert.That(reader.GetString(0), Has.Length.GreaterThan(5));
                Assert.That(reader.GetDecimal(1), Is.GreaterThanOrEqualTo(0));
            }
            Assert.That(reader.Read(), Is.False);
        }

        [Test]
        public virtual void Execute_WithInteger_ValidStatement()
        {
            var engine = Provider.GetRequiredService<GlobalEngine>();
            var reader = engine.ExecuteReader("SELECT COUNT(WindPark) AS CountRows FROM WindEnergy");
            Assert.That(reader, Is.Not.Null);
            Assert.That(reader.FieldCount, Is.EqualTo(1));
            Assert.That(reader.GetName(0), Is.EqualTo("CountRows"));
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.GetInt32(0), Is.EqualTo(2280));
            Assert.That(reader.Read(), Is.False);
        }

        [Test]
        public virtual void Execute_WithDateTime_ValidStatement()
        {
            var engine = Provider.GetRequiredService<GlobalEngine>();
            var reader = engine.ExecuteReader("SELECT Instant FROM WindEnergy ORDER BY Instant DESC LIMIT 1");
            Assert.That(reader, Is.Not.Null);
            Assert.That(reader.FieldCount, Is.EqualTo(1));
            Assert.That(reader.GetName(0), Is.EqualTo("Instant"));
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.GetDateTime(0), Is.EqualTo(new DateTime(2023, 1, 15, 23, 0, 0)));
            Assert.That(reader.Read(), Is.False);
        }

        [Test]
        public virtual void Execute_ShowAllTimeseries_ValidStatement()
        {
            var engine = Provider.GetRequiredService<GlobalEngine>();
            var reader = engine.ExecuteReader("SHOW TIMESERIES");
            Assert.That(reader, Is.Not.Null);
            Assert.That(reader.FieldCount, Is.EqualTo(1));
            Assert.That(reader.GetName(0), Is.EqualTo("TimeseriesName"));
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.GetString(0), Is.EqualTo("WindEnergy"));
            Assert.That(reader.Read(), Is.False);
        }

        [Test]
        public virtual void Execute_ShowTimeseriesWindEnergy_ValidStatement()
        {
            var engine = Provider.GetRequiredService<GlobalEngine>();
            var reader = engine.ExecuteReader("SHOW TIMESERIES WindEnergy");
            Assert.That(reader, Is.Not.Null);
            Assert.That(reader.FieldCount, Is.EqualTo(5));
            Assert.That(reader.GetName(0), Is.EqualTo("TimeseriesName"));
            Assert.That(reader.GetName(1), Is.EqualTo("ColumnName"));
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.GetString(0), Is.EqualTo("WindEnergy"));
            Assert.That(reader.GetString(1), Is.EqualTo("Instant"));
        }
    }
}
