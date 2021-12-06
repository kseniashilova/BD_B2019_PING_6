using System;
using System.Collections.Generic;

namespace Hse.DbGenerator.Utils
{
    public static class RandomExtensions
    {
        private static readonly Random Random = new();
        
        public static TElement GetRandomElement<TElement>(this List<TElement> collection)
        {
            return collection[Random.Next(0, collection.Count)];
        }

        public static DateTime GetRandomDate(DateTime startDate, DateTime endDate)
        {
            var timeSpan = endDate - startDate;
            var newSpan = new TimeSpan(0, Random.Next(0, (int)timeSpan.TotalMinutes), 0);
            return startDate + newSpan;
        }
    }
}