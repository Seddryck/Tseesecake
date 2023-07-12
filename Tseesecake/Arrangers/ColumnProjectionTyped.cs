using DubUrl.Querying.Dialects.Casters;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying;
using Tseesecake.Querying.Expressions;
using Tseesecake.Querying.Projections;
using Tseesecake.Querying.Slicers;

namespace Tseesecake.Arrangers
{
    internal class ColumnProjectionTyped : ISelectArranger
    {
        public void Execute(SelectStatement statement)
        {
            var projections = statement.Projections.Where(x => x is ColumnProjection).Cast<ColumnProjection>();
            if (!projections.Any())
                return;

            foreach (var projection in statement.Projections)
            {
                if (projections.Contains(projection))
                    ((ColumnExpression)((ColumnProjection)projection).Expression).Column 
                        = statement.Timeseries.Columns.Single(x => 
                            x.Name == ((ColumnExpression)((ColumnProjection)projection).Expression).Column.Name
                    );
            }
        }
    }
}
