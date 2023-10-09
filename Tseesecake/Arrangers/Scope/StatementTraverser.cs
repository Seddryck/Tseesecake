using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers.Action;
using Tseesecake.Modeling.Statements;

namespace Tseesecake.Arrangers.Scope
{
    internal class StatementTraverser : IScopeArranger
    {
        protected SelectStatement Statement { get; }

        public StatementTraverser(SelectStatement statement)
            => Statement = statement;

        public void Execute(IActionArranger action)
        {
            Statement.Accept(action);
        }
    }
}
