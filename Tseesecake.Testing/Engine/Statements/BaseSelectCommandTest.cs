using DubUrl.Mapping;
using DubUrl.Querying.Dialects;
using DubUrl.Registering;
using System;
using System.Collections.Generic;
using System.Linq;
using Tseesecake.Engine.Statements;

namespace Tseesecake.Testing.Engine.Statements
{
    public abstract class BaseSelectCommandTest
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
            => Assert.That(new SelectCommand(SelectStatementDefinition.ProjectionSingle).Read(Dialect, Connectivity)
                , Is.EqualTo(ProjectionSingle));

        protected abstract string ProjectionMultiple { get; }
        [Test]
        public void Read_ProjectionMultiple_ValidStatement()
            => Assert.That(new SelectCommand(SelectStatementDefinition.ProjectionMultiple).Read(Dialect, Connectivity)
                , Is.EqualTo(ProjectionMultiple));

        protected abstract string ProjectionExpression { get; }
        [Test]
        public virtual void Read_ProjectionExpression_ValidStatement()
            => Assert.That(new SelectCommand(SelectStatementDefinition.ProjectionExpression).Read(Dialect, Connectivity)
                , Is.EqualTo(ProjectionExpression));

        protected abstract string ProjectionAggregation { get; }
        [Test]
        public void Read_ProjectionAggregation_ValidStatement()
            => Assert.That(new SelectCommand(SelectStatementDefinition.ProjectionAggregation).Read(Dialect, Connectivity)
                , Is.EqualTo(ProjectionAggregation));

        protected abstract string ProjectionAggregationFilter { get; }
        [Test]
        public void Read_ProjectionAggregationFilter_ValidStatement()
            => Assert.That(new SelectCommand(SelectStatementDefinition.ProjectionAggregationFilter).Read(Dialect, Connectivity)
                , Is.EqualTo(ProjectionAggregationFilter));

        protected abstract string ProjectionWindow { get; }
        [Test]
        public void Read_ProjectionWindow_ValidStatement()
            => Assert.That(new SelectCommand(SelectStatementDefinition.ProjectionWindow).Read(Dialect, Connectivity)
                , Is.EqualTo(ProjectionWindow));

        protected abstract string ProjectionWindowOffset { get; }
        [Test]
        public void Read_ProjectionWindowOffset_ValidStatement()
            => Assert.That(new SelectCommand(SelectStatementDefinition.ProjectionWindowOffset).Read(Dialect, Connectivity)
                , Is.EqualTo(ProjectionWindowOffset));

        protected abstract string ProjectionWindowOffsetExpression { get; }
        [Test]
        public virtual void Read_ProjectionWindowOffsetExpression_ValidStatement()
            => Assert.That(new SelectCommand(SelectStatementDefinition.ProjectionWindowOffsetExpression).Read(Dialect, Connectivity)
                , Is.EqualTo(ProjectionWindowOffsetExpression));

        protected abstract string ProjectionWindowFrame { get; }
        [Test]
        public void Read_ProjectionWindowFrame_ValidStatement()
            => Assert.That(new SelectCommand(SelectStatementDefinition.ProjectionWindowFrame).Read(Dialect, Connectivity)
                , Is.EqualTo(ProjectionWindowFrame));

        protected abstract string FilterSingle { get; }
        [Test]
        public void Read_FilterSingle_ValidStatement()
            => Assert.That(new SelectCommand(SelectStatementDefinition.FilterSingle).Read(Dialect, Connectivity)
                , Is.EqualTo(FilterSingle));

        protected abstract string FilterMultiple { get; }
        [Test]
        public void Read_FilterMultiple_ValidStatement()
            => Assert.That(new SelectCommand(SelectStatementDefinition.FilterMultiple).Read(Dialect, Connectivity)
                , Is.EqualTo(FilterMultiple));

        protected abstract string FilterCuller { get; }
        [Test]
        public void Read_FilterCuller_ValidStatement()
            => Assert.That(new SelectCommand(SelectStatementDefinition.FilterCuller).Read(Dialect, Connectivity)
                , Is.EqualTo(FilterCuller));

        protected abstract string FilterTemporizer { get; }
        [Test]
        public void Read_FilterTemporizer_ValidStatement()
            => Assert.That(new SelectCommand(SelectStatementDefinition.FilterTemporizer).Read(Dialect, Connectivity)
                , Is.EqualTo(FilterTemporizer));

        protected abstract string SlicerSingle { get; }
        [Test]
        public void Read_SlicerSingle_ValidStatement()
            => Assert.That(new SelectCommand(SelectStatementDefinition.SlicerSingle).Read(Dialect, Connectivity)
                , Is.EqualTo(SlicerSingle));

        protected abstract string SlicerMultiple { get; }
        [Test]
        public void Read_SlicerMultiple_ValidStatement()
            => Assert.That(new SelectCommand(SelectStatementDefinition.SlicerMultiple).Read(Dialect, Connectivity)
                , Is.EqualTo(SlicerMultiple));

        protected abstract string SlicerAndGroupFilter { get; }
        [Test]
        public virtual void Read_SlicerAndGroupFilter_ValidStatement()
            => Assert.That(new SelectCommand(SelectStatementDefinition.SlicerAndGroupFilter).Read(Dialect, Connectivity)
                , Is.EqualTo(SlicerAndGroupFilter));

        protected abstract string NamedWindow { get; }
        [Test]
        public virtual void Read_NamedWindow_ValidStatement()
            => Assert.That(new SelectCommand(SelectStatementDefinition.NamedWindow).Read(Dialect, Connectivity)
                , Is.EqualTo(NamedWindow));

        protected abstract string Qualify { get; }
        [Test]
        public void Read_Qualify_ValidStatement()
            => Assert.That(new SelectCommand(SelectStatementDefinition.Qualify).Read(Dialect, Connectivity)
                , Is.EqualTo(Qualify));

        protected abstract string LimitOffset { get; }
        [Test]
        public void Read_LimitOffset_ValidStatement()
            => Assert.That(new SelectCommand(SelectStatementDefinition.LimitOffset).Read(Dialect, Connectivity)
                , Is.EqualTo(LimitOffset));

        protected abstract string VirtualMeasurementProjection { get; }
        [Test]
        public void Read_VirtualMeasurementProjection_ValidStatement()
            => Assert.That(new SelectCommand(SelectStatementDefinition.VirtualMeasurementProjection).Read(Dialect, Connectivity)
                , Is.EqualTo(VirtualMeasurementProjection));

        protected abstract string VirtualMeasurementAggregation { get; }
        [Test]
        public void Read_VirtualMeasurementAggregation_ValidStatement()
            => Assert.That(new SelectCommand(SelectStatementDefinition.VirtualMeasurementAggregation).Read(Dialect, Connectivity)
                , Is.EqualTo(VirtualMeasurementAggregation));
    }
}
