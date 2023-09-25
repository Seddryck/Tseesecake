using DubUrl.Extensions.DependencyInjection;
using DubUrl.Mapping;
using DubUrl.Querying.Dialects;
using DubUrl.Registering;
using DuckDB.NET.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tseesecake.Engine;
using Tseesecake.Testing.Engine;

namespace Tseesecake.QA.Postgresql
{
    [Category("Postgresql")]
    public class SelectCommandPostgresqlTest : BaseSelectCommandTest
    {
        public override string ConnectionString => $"pgsql://postgres:Password12!@localhost/Energy";

        private bool CheckPostgresqlVersion()
            => (Environment.GetEnvironmentVariable("APPVEYOR")?.ToLowerInvariant()) != "true";

        [Test]
        public override void Execute_ProjectionWindowOffset_ValidStatement()
        {
            if (!CheckPostgresqlVersion())
                Assert.Ignore("This version of Postgresql is not supporting some needed features");
            base.Execute_ProjectionWindowOffset_ValidStatement();
        }

        [Test]
        public override void Execute_ProjectionWindowOffsetExpression_ValidStatement()
        {
            if (!CheckPostgresqlVersion())
                Assert.Ignore("This version of Postgresql is not supporting some needed features");
            base.Execute_ProjectionWindowOffsetExpression_ValidStatement();
        }
    }
}