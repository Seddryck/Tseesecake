using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling
{
    public class UnexpectedTypeException<E> : TseesecakeException
    {
        public UnexpectedTypeException(Type type)
            : base($"The type '{type.Name}' is not supported. You must use a type implementing '{typeof(E).Name}'") { }
    }
}
