using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Tseesecake.Engine.Mounting;
using Tseesecake.Modeling;
using Tseesecake.Testing.Engine;

namespace Tseesecake.QA
{
    public abstract class BaseDmlStatementTest : BaseQATest
    {
        [OneTimeSetUp]
        public void SetupFixture()
            => SetupEngine(new[] { typeof(DmlEngine), typeof(SelectEngine) });

        [Test]
        public virtual void Mount_CreateOrReplace_ValidStatement()
        {
            var engine = Provider.GetRequiredService<DmlEngine>();
            engine.Mount(DmlStatementDefinition.CreateOrReplace);

            var count = engine.ExecuteScalar<long>(DmlStatementDefinition.ProjectionCount);
            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public virtual void Mount_CopyFrom_ValidStatement()
        {
            var engine = Provider.GetRequiredService<DmlEngine>();
            engine.Mount(DmlStatementDefinition.CopyFrom);

            var count = engine.ExecuteScalar<long>(DmlStatementDefinition.ProjectionCount);
            Assert.That(count, Is.EqualTo(2280));
        }
    }
}
