using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Modeling.Statements.Restrictions
{
    public interface IRestriction
    {
        string Template { get; }
    }
}
