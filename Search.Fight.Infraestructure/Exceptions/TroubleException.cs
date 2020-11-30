using System;

namespace Search.Fight.Infraestructure.Exceptions
{
    public class TroubleException : Exception
    {

        public TroubleException()
        {
        }

        public TroubleException(string message)
            : base(message)
        {
        }

        public TroubleException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}