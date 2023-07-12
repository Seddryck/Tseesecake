using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Querying;

namespace Tseesecake.Arrangers
{
    public interface IArranger<T>
    {
        void Execute(T statement);
    }

    public interface ISelectArranger : IArranger<SelectStatement>
    { }
}
