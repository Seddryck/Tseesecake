using DubUrl.Mapping;
using DubUrl.Querying.Dialects;
using DubUrl.Registering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tseesecake.Engine;
using Tseesecake.Modeling;
using Tseesecake.Querying;
using Tseesecake.Querying.Filters;
using Tseesecake.Querying.Ordering;
using Tseesecake.Querying.Restrictions;
using Tseesecake.Querying.Slicers;

namespace Tseesecake.Testing.Engine.DuckDB
{
    public abstract class BaseElementalQueryTest
    {
        protected IDialect Dialect { get; set; }
        protected IConnectivity Connectivity { get; set; }

        protected abstract string DialectName { get; }

        [OneTimeSetUp]
        public void Initialize()
        {
            //Provider registration
            new ProviderFactoriesRegistrator().Register();

            //Scheme mapping
            var schemeMapperBuilder = new SchemeMapperBuilder();
            schemeMapperBuilder.Build();
            var mapper = schemeMapperBuilder.GetMapper(DialectName);
            (Dialect, Connectivity) = (mapper.GetDialect(), mapper.GetConnectivity());
        }

        protected abstract string ProjectionSingle { get; }
        [Test]
        public virtual void Read_ProjectionSingle_ValidStatement()
            => Assert.That(new ElementalQuery(SelectStatementDefinition.ProjectionSingle).Read(Dialect, Connectivity)
                , Is.EqualTo(ProjectionSingle));

        protected abstract string ProjectionMultiple { get; }
        [Test]
        public void Read_ProjectionMultiple_ValidStatement()
            => Assert.That(new ElementalQuery(SelectStatementDefinition.ProjectionMultiple).Read(Dialect, Connectivity)
                , Is.EqualTo(ProjectionMultiple));

        protected abstract string ProjectionExpression { get; }
        [Test]
        public void Read_ProjectionExpression_ValidStatement()
            => Assert.That(new ElementalQuery(SelectStatementDefinition.ProjectionExpression).Read(Dialect, Connectivity)
                , Is.EqualTo(ProjectionExpression));

        protected abstract string ProjectionAggregation { get; }
        [Test]
        public void Read_ProjectionAggregation_ValidStatement()
            => Assert.That(new ElementalQuery(SelectStatementDefinition.ProjectionAggregation).Read(Dialect, Connectivity)
                , Is.EqualTo(ProjectionAggregation));

        protected abstract string ProjectionAggregationFilter { get; }
        [Test]
        public void Read_ProjectionAggregationFilter_ValidStatement()
            => Assert.That(new ElementalQuery(SelectStatementDefinition.ProjectionAggregationFilter).Read(Dialect, Connectivity)
                , Is.EqualTo(ProjectionAggregationFilter));

        protected abstract string ProjectionWindow { get; }
        [Test]
        public void Read_ProjectionWindow_ValidStatement()
            => Assert.That(new ElementalQuery(SelectStatementDefinition.ProjectionWindow).Read(Dialect, Connectivity)
                , Is.EqualTo(ProjectionWindow));

        protected abstract string ProjectionWindowOffset { get; }
        [Test]
        public void Read_ProjectionWindowOffset_ValidStatement()
            => Assert.That(new ElementalQuery(SelectStatementDefinition.ProjectionWindowOffset).Read(Dialect, Connectivity)
                , Is.EqualTo(ProjectionWindowOffset));

        protected abstract string FilterSingle { get; }
        [Test]
        public void Read_FilterSingle_ValidStatement()
            => Assert.That(new ElementalQuery(SelectStatementDefinition.FilterSingle).Read(Dialect, Connectivity)
                , Is.EqualTo(FilterSingle));

        protected abstract string FilterMultiple { get; }
        [Test]
        public void Read_FilterMultiple_ValidStatement()
            => Assert.That(new ElementalQuery(SelectStatementDefinition.FilterMultiple).Read(Dialect, Connectivity)
                , Is.EqualTo(FilterMultiple));

        protected abstract string FilterCuller { get; }
        [Test]
        public void Read_FilterCuller_ValidStatement()
            => Assert.That(new ElementalQuery(SelectStatementDefinition.FilterCuller).Read(Dialect, Connectivity)
                , Is.EqualTo(FilterCuller));

        protected abstract string FilterTemporizer { get; }
        [Test]
        public void Read_FilterTemporizer_ValidStatement()
            => Assert.That(new ElementalQuery(SelectStatementDefinition.FilterTemporizer).Read(Dialect, Connectivity)
                , Is.EqualTo(FilterTemporizer));

        protected abstract string SlicerSingle { get; }
        [Test]
        public void Read_SlicerSingle_ValidStatement()
            => Assert.That(new ElementalQuery(SelectStatementDefinition.SlicerSingle).Read(Dialect, Connectivity)
                , Is.EqualTo(SlicerSingle));

        protected abstract string SlicerMultiple { get; }
        [Test]
        public void Read_SlicerMultiple_ValidStatement()
            => Assert.That(new ElementalQuery(SelectStatementDefinition.SlicerMultiple).Read(Dialect, Connectivity)
                , Is.EqualTo(SlicerMultiple));

        protected abstract string SlicerAndGroupFilter { get; }
        [Test]
        public void Read_SlicerAndGroupFilter_ValidStatement()
            => Assert.That(new ElementalQuery(SelectStatementDefinition.SlicerAndGroupFilter).Read(Dialect, Connectivity)
                , Is.EqualTo(SlicerAndGroupFilter));

        protected abstract string LimitOffset { get; }
        [Test]
        public void Read_LimitOffset_ValidStatement()
            => Assert.That(new ElementalQuery(SelectStatementDefinition.LimitOffset).Read(Dialect, Connectivity)
                , Is.EqualTo(LimitOffset));
    }
}
