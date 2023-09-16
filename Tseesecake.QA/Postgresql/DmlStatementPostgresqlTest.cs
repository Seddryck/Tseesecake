using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tseesecake.Testing.Engine;

namespace Tseesecake.QA.Postgresql
{
    [Category("Postgresql")]
    public class DmlStatementPostgresqlTest : BaseDmlStatementTest
    {
        public override string ConnectionString => $"pgsql://postgres:Password12!@localhost/EnergyMounting";

        [Test]
        [Ignore("File options not correctly handled")]
        public override void Mount_CopyFrom_ValidStatement()
        { }
    }
}