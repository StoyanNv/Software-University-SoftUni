﻿namespace WebServer.Server.Handlers
{
    using HTTP.Contracts;
    using System;

    public class GetHandler :RequestHandler
    {
        public GetHandler(Func<IHttpRequest, IHttpResponse> handlingFunc) 
            : base(handlingFunc)
        {
        }
    }
}