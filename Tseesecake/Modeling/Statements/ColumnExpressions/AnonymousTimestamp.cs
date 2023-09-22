using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Modeling.Statements.ColumnExpressions
{
    internal class AnonymousTimestamp : Timestamp
    {
        public AnonymousTimestamp()
            : base("_anonymous_") { }
    }
}
