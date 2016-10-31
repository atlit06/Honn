using System;

namespace Assignment3.Services.Exceptions
{
    public class InvalidParametersException : Exception
    {
        public InvalidParametersException()
        {

        }
        public InvalidParametersException(string message) : base(message)
        {
        }
        public InvalidParametersException(string message, Exception inner) : base(message, inner)
        {
        }
    }    
}