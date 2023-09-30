using DubUrl.Querying.Dialects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Arrangers
{
    public interface IArrangerCollectionProvider
    {
        IArrangerCollectionFactory Get<T>() where T : IDialect;
        IArrangerCollectionFactory Get(Type dialect);
    }
}
