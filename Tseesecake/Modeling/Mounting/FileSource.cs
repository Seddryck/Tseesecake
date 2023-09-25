using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling.Mounting
{
    internal class FileSource : IFileSource
    {
        public string Path { get; }
        public string Extension { get; }
        public IDictionary<string, object> Options { get; }

        public FileSource(string path, IDictionary<string, object> options)
            => (Path, Extension, Options) = (path, System.IO.Path.GetExtension(path)[1..].ToUpper(), options);
    }
}
