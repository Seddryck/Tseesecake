using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.Expressions;

namespace Tseesecake.Modeling.Statements.Ordering
{
    internal class ColumnOrder : IOrderBy
    {
        public ColumnReference Reference { get; }
        public Sorting Sort { get; }
        public NullSorting NullSort { get; }

        public string Template { get => nameof(ColumnOrder); }

        public ColumnOrder(ColumnReference reference)
            : this(reference, Sorting.Ascending) { }

        public ColumnOrder(ColumnReference reference, Sorting sort)
            : this(reference, sort, NullSorting.First) { }

        public ColumnOrder(ColumnReference reference, Sorting sort, NullSorting nullSort)
            => (Reference, Sort, NullSort) = (reference, sort, nullSort);
    }
}
