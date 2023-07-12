using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling
{
    internal record class AnonymousTimestamp : Timestamp
    {
        public AnonymousTimestamp()
            : base("_anonymous_") { }
    }
}
