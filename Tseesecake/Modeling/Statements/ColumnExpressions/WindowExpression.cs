using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.Aggregations;
using Tseesecake.Modeling.Statements.Frames;
using Tseesecake.Modeling.Statements.WindowFunctions;
using Tseesecake.Modeling.Statements.Windows;

namespace Tseesecake.Modeling.Statements.ColumnExpressions
{
    internal class WindowExpression : BaseExpression
    {
        public IWindowFunction WindowFunction { get; }
        public IWindow Window { get; }
        public WindowExpression(IWindowFunction windowFunction, IWindow window)
            => (WindowFunction, Window) = (windowFunction, window);
    }
}
