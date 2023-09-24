using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling.Statements.Windows
{
    internal class ReferenceWindow : IWindow
    {
        public string Name { get; }

        public ReferenceWindow(string name)
            => Name = name;
    }
}
