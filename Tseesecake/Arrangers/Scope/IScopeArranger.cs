using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers.Action;
using Tseesecake.Modeling.Statements;

namespace Tseesecake.Arrangers.Scope
{
    public interface IScopeArranger
    {
        void Execute(IActionArranger action);
    }
}
