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
    public class ColumnForward : IArranger
    {
        public void Execute(SelectStatement statement)
        {
            var scope = new StatementTraverser(statement);
            var action = new ReplaceColumnReferenceByColumn(statement);
            scope.Execute(action);
        }
    }
}
