using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.Restrictions
{
    public interface IRestriction
    {
        string Template { get; }       
    }
}
