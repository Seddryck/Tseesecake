using DubUrl.Querying.Dialects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Engine.Statements.Common.Arrangers;
using Tseesecake.Engine.Statements.MsSqlServer.Arrangers;

namespace Tseesecake.Engine.Statements.MsSql
{
    [Dialect<TSqlDialect>()]
    internal class MsSqlServerArrangerCollectionFactory : BaseArrangerCollectionFactory
    {
        protected override IArranger[] InstantiateDialect()
            => new IArranger[] { 
                new MsSqlServerExpressionForward()
                , new NamedWindowForward()
            };
    }
}
