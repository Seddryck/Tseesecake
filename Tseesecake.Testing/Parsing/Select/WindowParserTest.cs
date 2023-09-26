using Newtonsoft.Json.Linq;
using Sprache;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.Aggregations;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Ordering;
using Tseesecake.Modeling.Statements.Slicers;
using Tseesecake.Modeling.Statements.WindowFunctions;
using Tseesecake.Modeling.Statements.Windows;
using Tseesecake.Parsing.Select;

namespace Tseesecake.Testing.Parsing.Select
{
    public class WindowParserTest
    {
        [Test]
        [TestCase("PARTITION BY WindPark", new[] { "WindPark" })]
        [TestCase("PARTITION BY WindPark, WindFarm", new[] { "WindPark", "WindFarm" })]
        [TestCase("", new string[] { })]
        public void Parse_PartitionBy_Window(string text, string[] args)
        {
            var window = WindowParser.Window.Parse($"OVER({text})");
            Assert.That(window, Is.TypeOf<Window>());
            if (args.Length == 0)
                Assert.That(((Window)window).PartitionBys, Is.Null);
            else
            {
                Assert.That(((Window)window).PartitionBys, Is.Not.Null);
                Assert.That(((Window)window).PartitionBys, Has.Length.EqualTo(args.Length));

                foreach (var partitionBy in ((Window)window).PartitionBys)
                    Assert.That(partitionBy, Is.TypeOf<FacetSlicer>());
                var names = ((Window)window).PartitionBys.Cast<FacetSlicer>().Select(x => x.Facet.Name);
                Assert.That(names, Is.EquivalentTo(args));
            }
        }

        [Test]
        [TestCase("ORDER BY WindPark", new[] { "WindPark" })]
        [TestCase("ORDER BY WindPark, WindFarm", new[] { "WindPark", "WindFarm" })]
        [TestCase("", new string[] { })]
        public void Parse_OrderBy_Window(string text, string[] args)
        {
            var window = WindowParser.Window.Parse($"OVER({text})");
            Assert.That(window, Is.TypeOf<Window>());
            if (args.Length == 0)
                Assert.That(((Window)window).OrderBys, Is.Null);
            else
            {
                Assert.That(((Window)window).OrderBys, Is.Not.Null);
                Assert.That(((Window)window).OrderBys, Has.Length.EqualTo(args.Length));

                foreach (var partitionBy in ((Window)window).OrderBys)
                    Assert.That(partitionBy, Is.TypeOf<ColumnOrder>());
                var names = ((Window)window).OrderBys.Cast<ColumnOrder>().Select(x => ((ColumnReference)x.Expression).Name);
                Assert.That(names, Is.EquivalentTo(args));
            }
        }
    }
}