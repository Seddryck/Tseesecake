using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Parsing.Query;
using Tseesecake.Querying.Expressions;
using Tseesecake.Querying.Filters;
using Tseesecake.Querying.Ordering;

namespace Tseesecake.Testing.Parsing.Query
{
    public class OrderByParserTest
    {
        [Test]
        [TestCase("Instant", 1)]
        [TestCase("Instant, Producer", 2)]
        [TestCase("Instant ASC, Producer", 2)]
        [TestCase("Instant ASC NULLS FIRST, Producer", 2)]
        [TestCase("Instant ASC NULLS FIRST, Producer DESC NULLS LAST", 2)]
        public void Parse_OrderByColumns_CorrectValue(string text, int count)
        {
            var orderBy = OrderByParser.OrderBy.Parse(text);
            Assert.That(orderBy, Is.Not.Null);
            Assert.That(orderBy.ToArray(), Has.Length.EqualTo(count));
        }

        [Test]
        [TestCase("Instant", Sorting.Ascending)]
        [TestCase("Instant ASC", Sorting.Ascending)]
        [TestCase("Instant DESC", Sorting.Descending)]
        public void Parse_OrderBySorting_CorrectValue(string text, Sorting sorting)
        {
            var orderBy = OrderByParser.OrderBy.Parse(text);
            Assert.That(orderBy, Is.Not.Null);
            Assert.That(orderBy.ElementAt(0).Sort, Is.EqualTo(sorting));
        }

        [Test]
        [TestCase("Instant", NullSorting.First)]
        [TestCase("Instant NULLS FIRST", NullSorting.First)]
        [TestCase("Instant NULLS LAST", NullSorting.Last)]
        public void Parse_OrderByNullSorting_CorrectValue(string text, NullSorting sorting)
        {
            var orderBy = OrderByParser.OrderBy.Parse(text);
            Assert.That(orderBy, Is.Not.Null);
            Assert.That(orderBy.ElementAt(0).NullSort, Is.EqualTo(sorting));
        }
    }
}
