using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Arrangers.Action;

namespace Tseesecake.Modeling.Statements
{
    public interface IArrangeable
    {
        void Accept(IActionArranger arranger);
    }
}
