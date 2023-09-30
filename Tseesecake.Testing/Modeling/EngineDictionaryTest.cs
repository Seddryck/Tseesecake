using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements;

namespace Tseesecake.Testing.Modeling
{
    public class EngineDictionaryTest
    {
        private interface ISubShowStatement : IShowStatement { }

        [Test]
        public void Add_UnexpectedType_Throws()
            => Assert.Throws<UnexpectedTypeException<IStatement>>(
                    () => new EngineDictionary<IDataReaderEngine>()
                                .Add(typeof(object), new CatalogEngine(Array.Empty<Timeseries>()))
                );

        [Test]
        public void Add_ExpectedType_Succeed()
            => Assert.DoesNotThrow(
                    () => new EngineDictionary<IDataReaderEngine>()
                                .Add(typeof(IShowStatement), new CatalogEngine(Array.Empty<Timeseries>()))
                );

        [Test]
        public void TryGetValue_UnexpectedType_Throws()
            => Assert.Throws<UnexpectedTypeException<IStatement>>(
                    () => new EngineDictionary<IDataReaderEngine>()
                                .TryGetValue(typeof(object), out var value)
                );

        [Test]
        public void TryGetValue_TypeNotFound_False()
        {
            var engines = new EngineDictionary<IDataReaderEngine>()
            {
                { typeof(IShowStatement), new CatalogEngine(Array.Empty<Timeseries>()) }
            };
            Assert.That(engines.TryGetValue<ISelectStatement>(out var value), Is.False);
            Assert.That(value, Is.Null);
        }

        [Test]
        public void TryGetValue_TypeFound_False()
        {
            var engines = new EngineDictionary<IDataReaderEngine>()
            {
                { typeof(IShowStatement), new CatalogEngine(Array.Empty<Timeseries>()) }
            };
            Assert.That(engines.TryGetValue<IShowStatement>(out var value), Is.True);
            Assert.That(value, Is.TypeOf<CatalogEngine>());
        }

        [Test]
        public void TryGetValue_TypeInheritedFound_False()
        {
            var engines = new EngineDictionary<IDataReaderEngine>()
            {
                { typeof(IShowStatement), new CatalogEngine(Array.Empty<Timeseries>()) }
            };
            Assert.That(engines.TryGetValue<ISubShowStatement>(out var value), Is.True);
            Assert.That(value, Is.TypeOf<CatalogEngine>());
        }
    }
}
