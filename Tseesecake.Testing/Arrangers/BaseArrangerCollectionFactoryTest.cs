using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Modeling.Statements;
using Tseesecake.Querying;

namespace Tseesecake.Testing.Arrangers
{
    public class BaseArrangerCollectionFactoryTest
    {
        internal class StubArrangerCollectionFactory : BaseArrangerCollectionFactory
        {
            protected override ISelectArranger[] InstantiateDialect() => Array.Empty<ISelectArranger>();
        }

        internal class StubArranger : ISelectArranger
        {
            public void Execute(SelectStatement statement) => throw new NotImplementedException();
        }

        internal class StubExtendedArrangerCollectionFactory : BaseArrangerCollectionFactory
        {
            protected override ISelectArranger[] InstantiateDialect() => new[] { new StubArranger() };
        }

        [Test]
        public void Instantiate_Stub_ReturnsPolyglot()
        {
            var factory = new StubArrangerCollectionFactory();
            var arrangers = factory.Instantiate<IStatement>();
            Assert.That(arrangers, Is.Not.Null.And.Not.Empty);
            Assert.That(arrangers.All(x => x.GetType().GetCustomAttribute<PolyglotAttribute>() != null), Is.True);
        }

        [Test]
        public void Instantiate_ExtendedStub_ReturnsPolyglotAndExtended()
        {
            var factory = new StubExtendedArrangerCollectionFactory();
            var arrangers = factory.Instantiate<IStatement>();
            Assert.That(arrangers, Is.Not.Null.And.Not.Empty);
            Assert.That(arrangers.Count(x => x.GetType().GetCustomAttribute<PolyglotAttribute>() == null), Is.EqualTo(1));
            Assert.That(arrangers.Count(x => x.GetType().GetCustomAttribute<PolyglotAttribute>() != null), Is.EqualTo(arrangers.Length - 1));
        }
    }
}
