using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling
{
    public interface IReference<T> where T : ICatalogItem
    {
        public string Name { get; }
    }
}
