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
    public abstract class ExpressionForward : IArranger
    {
        public void Execute(SelectStatement statement)
        {
            var clauses = ClausesSelect(statement);
            var action = new ReplaceExpressionByProjection(statement);
            
            foreach (var clause in clauses) 
                (clause as IArrangeable)?.Accept(action);
        }

        protected abstract object[] ClausesSelect(SelectStatement statement);
    }
}
