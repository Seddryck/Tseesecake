using DubUrl.Querying.Dialects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Modeling;

namespace Tseesecake.Engine.Statements.DuckDB
{
    [Dialect<DuckdbDialect>()]
    internal class DuckdbArrangerCollectionFactory : BaseArrangerCollectionFactory
    {
        protected override IArranger[] InstantiateDialect()
            => Array.Empty<IArranger>();
    }
}
