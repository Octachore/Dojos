using System;
using System.Linq;

namespace Utils
{
    public static class Guard
    {
        public static void In<T>(T token, params T[] args)
        {
            if (!args.Contains(token)) throw new ArgumentException("Token not found in array.");
        }
    }
}
