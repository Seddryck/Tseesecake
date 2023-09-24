using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.Frames;
using Tseesecake.Modeling.Statements.Ordering;
using Tseesecake.Modeling.Statements.Slicers;

namespace Tseesecake.Modeling.Statements.Windows
{
    public class Window : IWindow
    {
        public ISlicer[]? PartitionBys { get; }
        public IOrderBy[]? OrderBys { get; }
        public IFrame? Frame { get; }

        public Window(ISlicer[]? partitionBys, IOrderBy[]? orderBys, IFrame? frame = null)
            => (PartitionBys, OrderBys, Frame) = (partitionBys, orderBys, frame);
    }
}
