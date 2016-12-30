using System;
using System.Runtime.Serialization;

namespace MicroNet.SQLHelpers
{
    /// <summary>
    /// SQLHelper定制异常
    /// </summary>
    public class SQLHelperException : ApplicationException
    {
        public SQLHelperException(string message) : base(message)
        {
        }

        public SQLHelperException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected SQLHelperException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
