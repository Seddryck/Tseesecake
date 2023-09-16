using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Modeling;
using Tseesecake.Querying;
using Tseesecake.Querying.Aggregations;
using Tseesecake.Querying.Expressions;
using Tseesecake.Querying.Projections;
using Tseesecake.Querying.Slicers;
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
            Assert.That(projection, Is.TypeOf<ExpressionProjection>());
            Assert.That(((ExpressionProjection)projection).Alias, Is.EqualTo(ts.Timestamp.Name));
            Assert.That(((ExpressionProjection)projection).Expression, Is.TypeOf<BucketExpression>());

            var expression = (BucketExpression)((ExpressionProjection)projection).Expression;
            Assert.That(expression.Slicer, Is.EqualTo(slicer));
        }
    }
}
