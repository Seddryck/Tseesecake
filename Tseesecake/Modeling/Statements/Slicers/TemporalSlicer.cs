using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Modeling.Statements.Slicers
{
    internal abstract class TemporalSlicer : ISlicer
    {
        public Timestamp Timestamp { get; set; }

        public abstract string Template { get; }

        public TemporalSlicer(Timestamp timestamp)
            => Timestamp = timestamp;
    }
}
