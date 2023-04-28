using HandlebarsDotNet;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Querying;

namespace Tseesecake.Engine
{
    internal class Translator
    {
        public string Execute(SelectStatement query)
        {
            var stream = Assembly.GetAssembly(GetType())!.GetManifestResourceStream($"{GetType().Namespace}.DuckDb.SelectStatement.txt")!;
            using (var reader = new StreamReader(stream))
            {
                var source = reader.ReadToEnd();
                var handleBars = Handlebars.Create(new HandlebarsConfiguration { NoEscape = true });
                var template = handleBars.Compile(source);
                return template(query);
            }
        }
    }
}
