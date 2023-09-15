using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Arrangers
{
    public interface IArrangerCollectionFactory
    {
        ISelectArranger[] Instantiate<IStatement>();
    }
}
