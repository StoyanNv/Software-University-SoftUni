namespace WebServer.Server.Exceptions
{
    using System;

    public class BadRequestExcepticon : Exception
    {
        private const string InvalidRequestMessage = "Request is not valid.";

        public static void ThrowFromInvalidRequest() => throw new BadRequestExcepticon(InvalidRequestMessage);
        public BadRequestExcepticon(string message)
            : base(message)
        {

        }
    }
}
