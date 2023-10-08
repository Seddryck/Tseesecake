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
    public class FacetAsSlicer : IArranger
    {
        public void Execute(SelectStatement statement)
        {
            var action = new InsertFacetProjectionAsSlicer(statement);
            var slicers = action.Execute(statement.Slicers.ToArray());
            statement.Slicers.Clear();
            statement.Slicers.AddRange(slicers);
        }
    }
}
