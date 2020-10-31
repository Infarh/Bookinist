using System;

namespace System
{
    static class RandomExtensions
    {
        public static T NextItem<T>(this Random rnd, params T[] items) => items[rnd.Next(items.Length)];
    }
}
