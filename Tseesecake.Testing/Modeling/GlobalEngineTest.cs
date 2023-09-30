using DubUrl;
using DubUrl.Querying.Dialects;
using Sprache;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Modeling;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements;

namespace Tseesecake.Testing.Modeling
{
    public class GlobalEngineTest
    {
        private interface IFooBarStatement : IStatement { }
        private class FooBarStatement : IFooBarStatement { }
        private class FooBarParser
        {
            public readonly static Parser<IFooBarStatement> FooBar =
                Parse.IgnoreCase("FooBar").Text().Token().Return(new FooBarStatement());
        }

        private class FooBarEngine : IDataReaderEngine
        {
            public IDataReader ExecuteReader(IStatement statement)
            {
                var reader = new Mock<IDataReader>();
                reader.SetupSequence(r => r.Read()).Returns(true).Returns(false);
                reader.Setup(r => r.FieldCount).Returns(1);
                reader.Setup(r => r.GetString(It.IsAny<int>())).Returns("foobar");
                return reader.Object;
            }
        }

        [Test]
        public void Execute_FooBarCommand_AllOfThem()
        {
            var dialect = Mock.Of<IDialect>();
            var databaseUrl = Mock.Of<IDatabaseUrl>(db => db.Dialect == dialect);

            var factory = new Mock<IDatabaseUrlFactory>();
            factory.Setup(f => f.Instantiate(It.IsAny<string>())).Returns(databaseUrl);

            var arrangers = new Mock<IArrangerCollectionFactory>();
            arrangers.Setup(af => af.Instantiate<IStatement>()).Returns(Array.Empty<ISelectArranger>());
            var provider = new Mock<IArrangerCollectionProvider>();
            provider.Setup(a => a.Get(It.IsAny<Type>())).Returns(arrangers.Object);

            var statement = "FOOBAR";
            var engine = new GlobalEngine(factory.Object, "foo://bar/", Array.Empty<Timeseries>(), provider.Object);
            engine.Add<IFooBarStatement>(new FooBarEngine(), FooBarParser.FooBar);
            var reader = engine.ExecuteReader(statement);

            Assert.That(reader, Is.Not.Null );
            Assert.That(reader.FieldCount, Is.EqualTo(1));
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.GetString(0), Is.EqualTo("foobar"));
            Assert.That(reader.Read(), Is.False);
        }
    }
}