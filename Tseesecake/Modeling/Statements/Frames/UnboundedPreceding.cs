using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling.Statements.Frames
{
    internal class UnboundedPreceding : Boundary
    {
        public UnboundedPreceding()
            : base(null, "unbounded_preceding") { }
    }
}
