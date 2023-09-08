using DubUrl.Querying;
using DubUrl.Querying.Parametrizing;
using DubUrl.Querying.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Querying;

namespace Tseesecake.Engine
{
    public class ElementalQuery : EmbeddedSqlTemplateCommand
    {
        public ElementalQuery(SelectStatement statement, IQueryLogger? logger = null)
            : base(
                   $"{typeof(ElementalQuery).Namespace}.{nameof(ElementalQuery)}"
                  , $"{typeof(ElementalQuery).Namespace}"
                  , $"{typeof(ElementalQuery).Namespace}"
                  , new Dictionary<string, object?>() { { "statement", statement } }
                  , logger ?? NullQueryLogger.Instance
            )
        { }
    }
}
