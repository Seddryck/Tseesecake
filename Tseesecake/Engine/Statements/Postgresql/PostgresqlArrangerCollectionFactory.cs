using DubUrl.Querying.Dialects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Engine.Statements.Postgresql.Arrangers;

namespace Tseesecake.Engine.Statements.Postgresql
{
    [Dialect<PgsqlDialect>()]
    internal class PostgresqlArrangerCollectionFactory : BaseArrangerCollectionFactory
    {
        protected override IArranger[] InstantiateDialect()
            => new IArranger[] { new PostgresqlExpressionForward() };
    }
}
