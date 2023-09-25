using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Windows;

namespace Tseesecake.Arrangers
{
    internal class NamedWindowForwardedToProjection : ISelectArranger
    {
        public void Execute(SelectStatement statement)
        {
            if (statement.Windows is null || statement.Windows.Count == 0)
                return;

            var windows = statement.Projections.Where(x => x.Expression is WindowExpression);
            if (!windows.Any())
                return;

            for (int i = 0; i < statement.Projections.Count; i++)
            {
                if (statement.Projections[i].Expression is WindowExpression expression)
                {
                    if (expression.Window is ReferenceWindow refWindow)
                    {
                        var window = statement.Windows.SingleOrDefault(x => x.Name == refWindow.Name);
                        if (window != null)
                        {
                            expression.Window = new Window(
                                window.PartitionBys
                                , window.OrderBys
                                , window.Frame);
                        }
                    }
                }
            }
            statement.Windows.Clear();
        }
    }
}