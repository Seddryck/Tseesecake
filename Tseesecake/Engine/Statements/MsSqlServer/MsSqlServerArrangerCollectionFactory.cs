using DubUrl.Querying.Dialects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Modeling;

namespace Tseesecake.Engine.Statements.MsSql
{
    [Dialect<TSqlDialect>()]
    internal class MsSqlServerArrangerCollectionFactory : BaseArrangerCollectionFactory
    {
        protected override ISelectArranger[] InstantiateDialect()
            => new ISelectArranger[] { new ExpressionForwardedToHavingClause()
                        , new ExpressionForwardedToOrderByClause()
                        , new NamedWindowForwardedToProjection()
            };
    }
}
