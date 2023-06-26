using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.Projections
{
    internal class ColumnProjection : IProjection
    {
        public Column Column { get; }

        public string Template { get => nameof(ColumnProjection); }

        public ColumnProjection(Column column)
            => Column = column;
    }
}
