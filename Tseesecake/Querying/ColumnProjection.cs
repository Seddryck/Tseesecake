﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying
{
    internal class ColumnProjection : IProjection
    {
        public Column Column { get; }

        public string Label { get => Column.Name; }

        public ColumnProjection(Column column)
            => Column = column;
    }
}
