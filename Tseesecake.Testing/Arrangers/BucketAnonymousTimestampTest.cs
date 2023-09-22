using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Modeling.Statements.Arguments;
using Tseesecake.Modeling.Statements.ColumnExpressions;
using Tseesecake.Querying;
using Tseesecake.Modeling.Statements.Aggregations;
using Tseesecake.Querying.Expressions;
using Tseesecake.Querying.Projections;
using Tseesecake.Querying.Slicers;
using Tseesecake.Testing.Engine;

namespace Tseesecake.Testing.Arrangers
{
    public class BucketAnonymousTimestampTest
    {
        [Test]
        public void Execute_BucketByAnonymous_Named()
        {
            var ts = DmlStatementDefinition.WindEnergy;
            var statement = new SelectStatement(ts,
                Array.Empty<IProjection>(),
                null,
                new[] {
                    new CyclicTemporalSlicer(new AnonymousTimestamp(), CyclicTemporal.MonthOfYear)
                }
            );

            var arranger = new BucketAnonymousTimestamp();
            arranger.Execute(statement);

            Assert.That(statement.Slicers, Has.Count.EqualTo(1));
            var slicer = statement.Slicers.ElementAt(0);
            Assert.That(slicer, Is.TypeOf<CyclicTemporalSlicer>());
            Assert.Multiple(() =>
            {
                Assert.That(((CyclicTemporalSlicer)slicer).Timestamp, Is.Not.TypeOf<AnonymousTimestamp>());
                Assert.That(((CyclicTemporalSlicer)slicer).Timestamp.Name, Is.EqualTo(ts.Timestamp.Name));
            });
        }
    }
}
