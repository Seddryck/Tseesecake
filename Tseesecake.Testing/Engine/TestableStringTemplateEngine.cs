using DubUrl.Mapping;
using DubUrl.Querying.Dialects;
using DubUrl.Querying.Reading;
using DubUrl.Querying.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Engine;

namespace Tseesecake.Testing.Engine
{
    internal class TestableStringTemplateEngine : StringTemplateEngine
    {
        public string Source { get; }

        public TestableStringTemplateEngine(string source)
            => Source = source;

        public string Render(IDictionary<string, object?> parameters)
            => Render(Source, GetSubTemplates(), ReadDictionaries(), parameters, null);

        protected IDictionary<string, string> GetSubTemplates()
        {
            var asm = typeof(QueryEngine).Assembly;
            var resources = asm.GetManifestResourceNames().Where(x => x.Contains("Calculator") && x.EndsWith(".st"));
            var dico = new Dictionary<string, string>();
            foreach (var resource in resources) 
            { 
                var name = resource.Split('.')[^3];
                using var stream = asm.GetManifestResourceStream(resource) ?? throw new NullReferenceException();
                using var reader = new StreamReader(stream);
                dico.Add(name, reader.ReadToEnd());
            }
            return dico;
        }

        protected IDictionary<string, IDictionary<string, object?>> ReadDictionaries()
        {
            var asm = typeof(QueryEngine).Assembly;
            var resources = asm.GetManifestResourceNames().Where(x => x.EndsWith("dic.st"));
            var dico = new Dictionary<string, IDictionary<string, object?>>();
            foreach (var resource in resources)
                dico.Add(resource.Split('.')[^3], ReadDictionary(asm, resource));
            return dico;
        }

        protected virtual IDictionary<string, object?> ReadDictionary(Assembly asm, string resource)
        {
            var dico = new Dictionary<string, object?>();
            using var stream = asm.GetManifestResourceStream(resource) ?? throw new NullReferenceException();
            using var reader = new StreamReader(stream);
            while (reader.Peek() >= 0)
            {
                (var key, var value) = ParseDictionaryEntry(reader.ReadLine());
                if (!string.IsNullOrEmpty(key))
                    dico.Add(key, value);
            }
            return dico;
        }

        protected virtual (string?, object?) ParseDictionaryEntry(string? entry)
        {
            if (string.IsNullOrEmpty(entry))
                return (null, null);
            var separator = entry.IndexOf(':');
            if (separator == -1)
                return (null, null);
            var key = entry[..separator].Trim();
            if (key[0] == '\"' && key[^1] == '\"')
                key = key.Trim('\"');

            var rawValue = entry[(separator + 1)..].Trim();
            if (rawValue[0] == '\"' && rawValue[^1] == '\"')
                return (key, rawValue.Trim('\"'));
            else if (rawValue.All(c => char.IsDigit(c)))
                return (key, int.Parse(rawValue));
            else if (rawValue.All(c => char.IsDigit(c) || c == '.'))
                return (key, decimal.Parse(rawValue));
            else if (rawValue.Equals("true", StringComparison.InvariantCultureIgnoreCase) || rawValue.Equals("false", StringComparison.InvariantCultureIgnoreCase))
                return (key, bool.Parse(rawValue));
            else
                throw new ArgumentOutOfRangeException();
        }
    }
}
