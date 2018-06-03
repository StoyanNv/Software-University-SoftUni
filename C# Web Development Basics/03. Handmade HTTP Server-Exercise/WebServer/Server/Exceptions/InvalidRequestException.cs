namespace WebServer.Server.Exceptions
{
    using System;
    public class InvalidRequestException : Exception
    {
        public InvalidRequestException(string message)
            : base(message)
        {
        }
    }
}