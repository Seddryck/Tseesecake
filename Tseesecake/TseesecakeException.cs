using System;
using System.Reflection;

namespace Tseesecake;

public abstract class TseesecakeException : ApplicationException
{
    public TseesecakeException(string message)
         : base(message)
    { }

    public TseesecakeException(string message, Exception innerException)
         : base(message, innerException)
    { }
}