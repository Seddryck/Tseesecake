using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Arrangers.Action;
using Tseesecake.Modeling.Statements.Aggregations;
using Tseesecake.Modeling.Statements.Frames;
using Tseesecake.Modeling.Statements.WindowFunctions;
using Tseesecake.Modeling.Statements.Windows;

namespace Tseesecake.Modeling.Statements.Expressions
{
    internal class WindowExpression : BaseExpression, IArrangeable
    {
        public IWindowFunction WindowFunction { get; }
        public IWindow Window { get; set; }
        public WindowExpression(IWindowFunction windowFunction, IWindow window)
            => (WindowFunction, Window) = (windowFunction, window);

        public void Accept(IActionArranger arranger)
        {
            if (arranger is IActionArranger<IWindow> windowArranger)
                Window = windowArranger.Execute(Window);
            (WindowFunction as IArrangeable)?.Accept(arranger);
            (Window as IArrangeable)?.Accept(arranger);
        }
    }
}
