using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling.Statements.Frames
{
    internal class RangeBetween : IFrame
    {
        public Boundary Lower { get; }
        public Boundary Upper { get; }

        public string Template { get => nameof(RangeBetween); }

        public RangeBetween(Boundary lower, Boundary upper)
            => (Lower, Upper) = (lower, upper);
    }
}
