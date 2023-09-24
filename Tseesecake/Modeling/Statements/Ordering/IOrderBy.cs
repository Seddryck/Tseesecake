using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Modeling.Statements.Ordering
{
    public interface IOrderBy
    {
        string Template { get; }
        Sorting Sort { get; }
        NullSorting NullSort { get; }
    }

    public enum Sorting
    {
        Ascending, Descending
    }

    public enum NullSorting
    {
        First, Last
    }
}
