using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using IS.Lab7.BL.Helpers;

namespace IS.Lab7.BL.StatCounting
{
    public static class TextStatCounter
    {
        
        public static IDictionary<string,double> GetBigramStats(string text)
        {
            text = TextPreprocessingHelper.HardPreprocessText(text);
                
            var bigrams = new List<string>();
            for (int i = 1; i < text.Length-1; i++)
            {
                var bigram1 = $"{text[i - 1]}{text[i]}";
                var bigram2 = $"{text[i]}{text[i+1]}";
                bigrams.Add(bigram1);
                bigrams.Add(bigram2);
            }

            var dict = new Dictionary<string, double>();
            foreach (var gram in bigrams.Distinct())
            {
                try
                {
                    dict.Add(gram, Convert.ToDouble(Regex.Matches(text, gram).Count) / Convert.ToDouble(bigrams.Count));
                }
                catch (Exception e)
                {
                    //ignore
                }
            }

            var temp = dict.OrderByDescending(i => i.Value).ToList();
            dict.Clear();
            temp.ForEach(i => dict.Add(i.Key, i.Value));
            return dict;
        }

        public static IDictionary<char, double> GetLetterStats(string text)
        {
            text = TextPreprocessingHelper.HardPreprocessText(text);
            var dict = new Dictionary<char, double>();
            for (var c = 'A'; c < 'Z'; c++)
            {
                dict.Add(c,Convert.ToDouble(text.Count(i=>i==c))/Convert.ToDouble(text.Length));
            }
            var temp = dict.OrderByDescending(i => i.Value).ToList();
            dict.Clear();
            temp.ForEach(i => dict.Add(i.Key, i.Value));
            return dict;
        }
    }
}