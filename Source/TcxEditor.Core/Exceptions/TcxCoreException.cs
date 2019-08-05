using System;

namespace TcxEditor.Core.Exceptions
{
    [Serializable]
    public class TcxCoreException : Exception
    {
        public TcxCoreException() { }
        public TcxCoreException(string message) : base(message) { }
        public TcxCoreException(string message, Exception inner) : base(message, inner) { }
        protected TcxCoreException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
