using System;
using System.Collections.Generic;
using System.Linq;
using IS.Lab7.BL.Helpers;

namespace IS.Lab7.BL.SymbolReplacement
{
    public static class SymbolReplacementKeyFactory
    {
        private const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static SymbolReplacementKeyDto Generate()
        {
            var shuffle = Letters.ToList().Shuffle();
            var replacements = new Dictionary<char, char>();
            for (var i = 0; i < Letters.Length; i++)
            {
                replacements.Add(Letters[i],shuffle[i]);
            }

            return new SymbolReplacementKeyDto()
            {
                replacements = replacements
            };
        }
    }
}