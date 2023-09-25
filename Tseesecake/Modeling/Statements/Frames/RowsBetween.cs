using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling.Statements.Frames
{
    internal class RowsBetween : IFrame
    {
        public Boundary Lower { get; }
        public Boundary Upper { get; }

        public string Template { get => nameof(RowsBetween); }

        public RowsBetween(Boundary lower, Boundary upper)
            => (Lower, Upper) = (lower, upper);
    }
}
