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
    public class ElementalQueryPostgresqlTest : BaseElementalQueryTest
    {
        public override string ConnectionString => $"pgsql://postgres:Password12!@localhost/Energy";
    }
}
