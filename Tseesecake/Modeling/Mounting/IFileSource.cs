using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling.Mounting
{
    public interface IFileSource
    {
        string Path { get; }
        string Extension { get; }
        IDictionary<string, object> Options { get; }
    }
}
