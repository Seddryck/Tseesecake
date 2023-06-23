using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.Ordering
{
    internal class ColumnOrder : IOrderBy
    {
        public Column Column { get; }
        public Sorting Sort { get; }
        public NullSorting NullSort { get; }

        public string Template { get => nameof(ColumnOrder); }

        public ColumnOrder(Column column)
            : this(column, Sorting.Ascending) { }

        public ColumnOrder(Column column, Sorting sort)
            : this(column, sort, NullSorting.First) { }

        public ColumnOrder(Column column, Sorting sort, NullSorting nullSort)
            => (Column, Sort, NullSort) = (column, sort, nullSort);
    }
}
