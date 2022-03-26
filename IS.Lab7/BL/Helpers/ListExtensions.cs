using System;
using System.Collections.Generic;

namespace IS.Lab7.BL.Helpers
{
    public static class ListExtensions
    {
        private static readonly Random Rng = new();  

        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            var copy = new List<T>(list);
            var n = copy.Count;  
            while (n > 1) 
            {  
                n--;  
                var k = Rng.Next(n + 1);  
                (copy[k], copy[n]) = (copy[n], copy[k]);
            }

            return copy;
        }
    }
}