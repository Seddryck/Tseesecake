using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Querying.Frames;
using Tseesecake.Querying.Ordering;
using Tseesecake.Querying.Slicers;

namespace Tseesecake.Querying.Windows
{
    public class NamedWindow
    {
        public string Name { get; }
        public ISlicer[]? PartitionBys { get; }
        public IOrderBy[]? OrderBys { get; }
        public IFrame? Frame { get; }

        public NamedWindow(string name, ISlicer[]? partitionBys, IOrderBy[]? orderBys, IFrame? frame)
            => (Name, PartitionBys, OrderBys, Frame) = (name, partitionBys, orderBys, frame);
    }
}
