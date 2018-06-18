namespace HTTPServer.Server.Common
{
    using System;

    public static class CoreValidator
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
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException($"{name} cannot be null or empty.", name);
            }
        }

        public static void ThrowIfNotLongEnough(string text, string name, int minLength)
        {
            if (string.IsNullOrEmpty(text) || text.Length < minLength)
            {
                throw new ArgumentException($"{name} cannot be null or less than {minLength} symbols long.", name);
            }
        }

        public static void ThrowIfNotEqualStrings(string first, string second, string firstName, string secondName)
        {
            if (first != second)
            {
                throw new ArgumentException($"The fields {firstName} and {secondName} don't match", firstName);
            }
        }
    }
}