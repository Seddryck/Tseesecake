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
    public class NamedWindow : Window
    {
        public string Name { get; }

        public NamedWindow(string name, Window window)
            : this(name, window.PartitionBys, window.OrderBys, window.Frame) { }

        public NamedWindow(string name, ISlicer[]? partitionBys, IOrderBy[]? orderBys, IFrame? frame)
            : base(partitionBys, orderBys, frame) { Name = name; }
    }
}
