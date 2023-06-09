﻿using System;
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
    public class FacetProjectionAsSlicerTest
    {
        [Test]
        public void Execute_TwoFacetsOneAggregation_TwoSlicers()
        {
            var ts = DmlStatementDefinition.WindEnergy;
            var statement = new SelectStatement(ts,
                new IProjection[] {
                    new ColumnProjection(ts.Timestamp)
                    , new ColumnProjection(ts.Facets.ElementAt(0))
                    , new ColumnProjection(ts.Facets.ElementAt(1))
                    , new AggregationProjection(new MaxAggregation(new ColumnExpression(ts.Measurements.ElementAt(0))), "Maximum")
                }
            );

            var arranger = new FacetProjectionAsSlicer();
            arranger.Execute(statement);

            Assert.That(statement.Projections, Has.Count.EqualTo(4));
            Assert.That(statement.Slicers, Has.Count.EqualTo(2));
            foreach (var newSlicer in statement.Slicers)
                Assert.That(newSlicer, Is.TypeOf<FacetSlicer>());
            var slicerNames = statement.Slicers.Where(x => x is FacetSlicer).Cast<FacetSlicer>().Select(x => x.Facet.Name);
            Assert.That(slicerNames, Is.EquivalentTo(ts.Facets.Select(x => x.Name)));
        }

        [Test]
        public void Execute_NoAggregation_NoSlicer()
        {
            var ts = DmlStatementDefinition.WindEnergy;
            var statement = new SelectStatement(ts,
                new IProjection[] {
                    new ColumnProjection(ts.Timestamp)
                    , new ColumnProjection(ts.Facets.ElementAt(0))
                    , new ColumnProjection(ts.Facets.ElementAt(1))
                    , new ColumnProjection(ts.Measurements.ElementAt(0))
                }
            );

            var arranger = new FacetProjectionAsSlicer();
            arranger.Execute(statement);

            Assert.That(statement.Projections, Has.Count.EqualTo(4));
            Assert.That(statement.Slicers, Has.Count.EqualTo(0));
        }

        [Test]
        public void Execute_PreExistingSlicer_ConservedButMissingAdded()
        {
            var ts = DmlStatementDefinition.WindEnergy;
            var statement = new SelectStatement(ts,
                new IProjection[] {
                    new ColumnProjection(ts.Timestamp)
                    , new ColumnProjection(ts.Facets.ElementAt(0))
                    , new ColumnProjection(ts.Facets.ElementAt(1))
                    , new AggregationProjection(new MaxAggregation(new ColumnExpression(ts.Measurements.ElementAt(0))), "Maximum")
                }, null,
                new ISlicer[] {
                    new CyclicTemporalSlicer(ts.Timestamp, CyclicTemporal.MonthOfYear),
                    new FacetSlicer(ts.Facets.ElementAt(0))
                }
            );

            var arranger = new FacetProjectionAsSlicer();
            arranger.Execute(statement);

            Assert.That(statement.Projections, Has.Count.EqualTo(4));
            Assert.That(statement.Slicers, Has.Count.EqualTo(3));
            var slicerNames = statement.Slicers.Where(x => x is FacetSlicer).Cast<FacetSlicer>().Select(x => x.Facet.Name);
            Assert.That(slicerNames, Is.EquivalentTo(ts.Facets.Select(x => x.Name)));
        }
    }
}
