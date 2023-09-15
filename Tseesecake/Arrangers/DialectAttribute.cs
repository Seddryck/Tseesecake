using DubUrl.Querying.Dialects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Arrangers
{
    public class DialectAttribute<T> : DialectAttribute where T : IDialect
    {
        public DialectAttribute()
            : base(typeof(T)) { }
    }

    public abstract class DialectAttribute : Attribute
    {
        public Type Dialect { get;  }
        public DialectAttribute(Type dialect)
            => Dialect = dialect;
    }
}
