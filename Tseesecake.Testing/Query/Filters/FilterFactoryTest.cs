using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Querying.Filters;

namespace Tseesecake.Testing.Query.Filters
{
    public class FilterFactoryTest
    {
        [Test]
        public void Instantiate_After_CorrectInstance()
        {
            var factory = new FilterFactory();
            var filter = factory.Instantiate("Temporizer", "After", "Instant", new object[] { new DateTime(2023, 1, 1) });
            Assert.That(filter, Is.Not.Null);
            Assert.That(filter, Is.TypeOf<AfterTemporizer>());

            var afterTemporizer = (AfterTemporizer)filter;
            Assert.That(afterTemporizer.Timestamp.Name, Is.EqualTo("Instant"));
            Assert.That(afterTemporizer.Instant, Is.EqualTo(new DateTime(2023, 1, 1)));
        }

        [Test]
        public void Instantiate_Range_CorrectInstance()
        {
            var factory = new FilterFactory();
            var filter = factory.Instantiate("Temporizer", "Range", "Instant", new object[] { new DateTime(2023, 1, 1), new DateTime(2023, 2, 1) });
            Assert.That(filter, Is.Not.Null);
            Assert.That(filter, Is.TypeOf<RangeTemporizer>());

            var rangeTemporizer = (RangeTemporizer)filter;
            Assert.That(rangeTemporizer.Timestamp.Name, Is.EqualTo("Instant"));
            Assert.That(rangeTemporizer.Start, Is.EqualTo(new DateTime(2023, 1, 1)));
            Assert.That(rangeTemporizer.End, Is.EqualTo(new DateTime(2023, 2, 1)));
        }

        [Test]
        public void Instantiate_EqualDicer_CorrectInstance()
        {
            var factory = new FilterFactory();
            var filter = factory.Instantiate("Dicer", "Equal", "Producer", new object[] { "Future Energy" });
            Assert.That(filter, Is.Not.Null);
            Assert.That(filter, Is.TypeOf<EqualDicer>());

            var typedFilter = (EqualDicer)filter;
            Assert.That(typedFilter.Facet.Name, Is.EqualTo("Producer"));
            Assert.That(typedFilter.Value, Is.EqualTo("Future Energy"));
        }

        [Test]
        public void Instantiate_InDicer_CorrectInstance()
        {
            var factory = new FilterFactory();
            var filter = factory.Instantiate("Dicer", "In", "Producer", new object[] { new[] { "Future Energy", "We can!" } });
            Assert.That(filter, Is.Not.Null);
            Assert.That(filter, Is.TypeOf<InDicer>());

            var typedFilter = (InDicer)filter;
            Assert.That(typedFilter.Facet.Name, Is.EqualTo("Producer"));
            Assert.That(typedFilter.Values, Does.Contain("Future Energy"));
            Assert.That(typedFilter.Values, Does.Contain("We can!"));
        }
    }
}
