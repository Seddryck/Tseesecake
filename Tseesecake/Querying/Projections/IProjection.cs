using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Querying.Projections
{
    public interface IProjection
    {
        string Template { get; }
        string Alias { get; }
    }
}
