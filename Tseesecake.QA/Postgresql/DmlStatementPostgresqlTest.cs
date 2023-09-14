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
        public override string ConnectionString => $"pgsql://localhost/WindEnergy";
    }
}