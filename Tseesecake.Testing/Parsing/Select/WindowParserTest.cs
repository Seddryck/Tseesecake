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
using Tseesecake.Modeling.Statements.Frames;
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
            var window = WindowParser.InlineWindow.Parse($"OVER({text})");
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
            Assert.That(((Window)window).OrderBys, Is.Null);
        }

        [Test]
        [TestCase("ORDER BY WindPark", new[] { "WindPark" })]
        [TestCase("ORDER BY WindPark, WindFarm", new[] { "WindPark", "WindFarm" })]
        [TestCase("", new string[] { })]
        public void Parse_OrderBy_Window(string text, string[] args)
        {
            var window = WindowParser.InlineWindow.Parse($"OVER({text})"); 
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
            Assert.That(((Window)window).PartitionBys, Is.Null);
        }

        [Test]
        public void Parse_PartitionByAndOrderBy_Window()
        {
            var window = WindowParser.InlineWindow.Parse($"OVER(PARTITION BY WindPark, WindFarm ORDER BY Instant)");
            Assert.That(window, Is.TypeOf<Window>());
            Assert.That(((Window)window).PartitionBys, Is.Not.Null);
            Assert.That(((Window)window).PartitionBys, Has.Length.EqualTo(2));
            Assert.That(((Window)window).OrderBys, Is.Not.Null);
            Assert.That(((Window)window).OrderBys, Has.Length.EqualTo(1));
        }

        [Test]
        public void Parse_Frame_Window()
        {
            var window = WindowParser.InlineWindow.Parse($"OVER(ORDER BY Instant ROWS UNBOUNDED PRECEDING)");
            Assert.That(window, Is.TypeOf<Window>());
            Assert.That(((Window)window).PartitionBys, Is.Null);
            Assert.That(((Window)window).OrderBys, Is.Not.Null);
            Assert.That(((Window)window).OrderBys, Has.Length.EqualTo(1));
            Assert.That(((Window)window).Frame, Is.Not.Null);
            Assert.That(((Window)window).Frame, Is.TypeOf<RowsSingle>());
        }

        [Test]
        [TestCase("OVER Seven", "Seven")]
        public void Parse_Reference_Window(string text, string identifier)
        {
            var window = WindowParser.ReferenceWindow.Parse(text);
            Assert.That(window, Is.TypeOf<ReferenceWindow>());
            Assert.That(((ReferenceWindow)window).Name, Is.EqualTo(identifier));
        }

        [Test]
        [TestCase("OVER (PARTITION BY WindPark)", typeof(Window))]
        [TestCase("OVER Seven", typeof(ReferenceWindow))]
        public void Parse_Over_Window(string text, Type type)
            => Assert.That(WindowParser.Window.Parse(text), Is.TypeOf(type));

        [Test]
        public void Parse_NamedWindow_NamedWindow()
        {
            var window = WindowParser.NamedWindow.Parse(
                "Seven AS PARTITION BY WindPark, WindFarm ORDER BY Instant"
                );
            Assert.That(window, Is.TypeOf<NamedWindow>());
            Assert.That(window.Name, Is.EqualTo("Seven"));
            Assert.That(window.PartitionBys, Is.Not.Null);
            Assert.That(window.PartitionBys, Has.Length.EqualTo(2));
            Assert.That(window.OrderBys, Is.Not.Null);
            Assert.That(window.OrderBys, Has.Length.EqualTo(1));
            Assert.That(window.Frame, Is.Null);
        }

        [Test]
        public void Parse_NamedWindowWithFrame_NamedWindow()
        {
            var window = WindowParser.NamedWindow.Parse(
                "Seven AS PARTITION BY WindPark, WindFarm ORDER BY Instant ROWS BETWEEN 5 PRECEDING AND CURRENT ROW"
                );
            Assert.That(window, Is.TypeOf<NamedWindow>());
            Assert.That(window.Name, Is.EqualTo("Seven"));
            Assert.That(window.PartitionBys, Is.Not.Null);
            Assert.That(window.PartitionBys, Has.Length.EqualTo(2));
            Assert.That(window.OrderBys, Is.Not.Null);
            Assert.That(window.OrderBys, Has.Length.EqualTo(1));
            Assert.That(window.Frame, Is.Not.Null);
            Assert.That(window.Frame, Is.TypeOf<RowsBetween>());
        }
    }
}