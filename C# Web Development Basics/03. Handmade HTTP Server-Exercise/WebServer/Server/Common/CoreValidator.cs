﻿namespace WebServer.Server.Common
{
    using System;

    public class CoreValidator
    {
        public static void ThrowIfNull(object obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }
        public static void ThrowIfNullOrEmpty(string text, string name)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException($"{name} cannot be null or empty.",name);
            }
        }
    }
}
