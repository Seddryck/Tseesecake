using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers.Action;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Frames;
using Tseesecake.Modeling.Statements.Ordering;
using Tseesecake.Modeling.Statements.Slicers;

namespace Tseesecake.Modeling.Statements.Windows
{
    public class Window : IWindow, IArrangeable
    {
        public ISlicer[]? PartitionBys { get; }
        public IOrderBy[]? OrderBys { get; }
        public IFrame? Frame { get; }

        public Window(ISlicer[]? partitionBys, IOrderBy[]? orderBys, IFrame? frame = null)
            => (PartitionBys, OrderBys, Frame) = (partitionBys, orderBys, frame);

        public void Accept(IActionArranger arranger)
        {
            foreach (var partitionBy in PartitionBys ?? Array.Empty<ISlicer>())
                (partitionBy as IArrangeable)?.Accept(arranger);

            foreach (var orderBy in OrderBys ?? Array.Empty<IOrderBy>())
                (orderBy as IArrangeable)?.Accept(arranger);
        }
    }
}
