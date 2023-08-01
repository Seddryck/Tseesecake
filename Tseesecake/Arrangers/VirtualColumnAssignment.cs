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
    internal class VirtualColumnAssignment : ISelectArranger
    {
        public void Execute(SelectStatement statement)
        {
            var projections = statement.Projections
                .Where(x => x is ColumnReferenceProjection).Cast<ColumnReferenceProjection>()
                .Where(x => statement.VirtualMeasurements.Select(y => y.Name).Contains(((ColumnExpression)x.Expression).Reference.Name));

            if (!projections.Any())
                return;

            foreach (var projection in statement.Projections)
            {
                if (projections.Contains(projection))
                    ((ColumnReferenceProjection)projection).Expression
                        = new VirtualColumnExpression(statement.VirtualMeasurements.Single(x => 
                            x.Name == ((ColumnExpression)((ColumnReferenceProjection)projection).Expression).Reference.Name
                    ).Expression!);
            }
        }
    }
}
