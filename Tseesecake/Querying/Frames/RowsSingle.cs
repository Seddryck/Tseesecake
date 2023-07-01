using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Querying.Filters;

namespace Tseesecake.Querying.Frames
{
    internal class RowsSingle : IFrame
    {
        public Boundary Single { get; }

        public string Template { get => nameof(RowsSingle); }

        public RowsSingle(Boundary single)
            => (Single) = (single);
    }
}
