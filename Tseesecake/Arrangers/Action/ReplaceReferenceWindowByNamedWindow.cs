using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Windows;

namespace Tseesecake.Arrangers.Action
{
    internal class ReplaceReferenceWindowByNamedWindow : IActionArranger<IWindow>
    {
        protected NamedWindow[] NamedWindows { get; }

        public ReplaceReferenceWindowByNamedWindow(SelectStatement statement)
            => NamedWindows = statement.Windows.Cast<NamedWindow>().ToArray();

        public IWindow Execute(IWindow obj)
        {
            if (obj is ReferenceWindow reference
                 && NamedWindows.Any(x => x.Name == reference.Name))
            {
                var window =NamedWindows.Single(x => x.Name == reference.Name);
                return new Window(
                                window.PartitionBys
                                , window.OrderBys
                                , window.Frame);
            }
            return obj;
        }

        public object Execute(object obj)
        {
            if (obj is IWindow window)
                Execute(window);
            return obj;
        }
    }
}