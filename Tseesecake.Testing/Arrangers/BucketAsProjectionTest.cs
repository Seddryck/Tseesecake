using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Arguments;
using Tseesecake.Modeling.Statements.ColumnExpressions;
using Tseesecake.Modeling.Statements.Projections;
using Tseesecake.Modeling.Statements.Slicers;
using Tseesecake.Testing.Engine;

namespace Tseesecake.Testing.Arrangers
{
    public class BucketAsProjectionTest
    {
        [Test]
        public void Execute_BucketByAnonymous_Named()
        {
            var ts = DmlStatementDefinition.WindEnergy;
            var slicer = new CyclicTemporalSlicer(ts.Timestamp, CyclicTemporal.MonthOfYear);
            var statement = new SelectStatement(ts,
                Array.Empty<IProjection>(),
                null,
                new[] { slicer }
            );

            var arranger = new BucketAsProjection();
            arranger.Execute(statement);

            Assert.That(statement.Projections, Has.Count.EqualTo(1));
            var projection = statement.Projections.ElementAt(0);
            Assert.That(((Projection)projection).Alias, Is.EqualTo(ts.Timestamp.Name));
            Assert.That(((Projection)projection).Expression, Is.TypeOf<BucketExpression>());

            var expression = (BucketExpression)((Projection)projection).Expression;
            Assert.That(expression.Slicer, Is.EqualTo(slicer));
        }
    }
}
