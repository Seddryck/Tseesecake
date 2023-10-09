using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Arrangers.Action
{
    public interface IActionArranger
    {
        object Execute(object obj);
    }

    public interface IActionArranger<T> : IActionArranger
    {
        T Execute(T obj);
    }
}
