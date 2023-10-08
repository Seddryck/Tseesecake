using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Arrangers.Action;
using Tseesecake.Arrangers.Scope;
using Tseesecake.Modeling.Statements;

namespace Tseesecake.Engine.Statements.Common.Arrangers
{
    [Polyglot]
    internal class BucketAsProjection : IArranger
    {
        public void Execute(SelectStatement statement)
        {
            var action = new InsertBucketAsProjection(statement);
            var projections = action.Execute(statement.Projections.ToArray());
            statement.Projections.Clear();
            statement.Projections.AddRange(projections);
        }
    }
}
