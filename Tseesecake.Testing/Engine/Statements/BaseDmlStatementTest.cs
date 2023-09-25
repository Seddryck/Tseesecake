using DubUrl.Mapping;
using DubUrl.Querying.Dialects;
using DubUrl.Registering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tseesecake.Engine.Mounting;

namespace Tseesecake.Testing.Engine.Statements
{
    public abstract class BaseDmlStatementTest
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

        protected abstract string CreateOrReplace { get; }
        [Test]
        public virtual void Read_CreateOrReplace_ValidStatement()
            => Assert.That(new DmlCommand(DmlStatementDefinition.CreateOrReplace).Read(Dialect, Connectivity)
                , Is.EqualTo(CreateOrReplace));

        protected abstract string CopyFrom { get; }
        [Test]
        public void Read_CopyFrom_ValidStatement()
            => Assert.That(new DmlCommand(DmlStatementDefinition.CopyFrom).Read(Dialect, Connectivity)
                , Is.EqualTo(CopyFrom));
    }
}
