using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using IS.Lab7.BL.StatCounting;

namespace IS.Lab7.UIHelpers
{
    public static class AvaloniaHelpers
    {
        public static void FillStats(string text, ListBox lettersStatsLB, ListBox bigramStatsLB)
        {
            
            
            var letterStats2 = TextStatCounter.GetLetterStats(text);
            var bigramStats2 = TextStatCounter.GetBigramStats(text);
            
            lettersStatsLB.Items = letterStats2.Select(i => new ListBoxItem()
            {
                Content = $"{i.Key} - {Convert.ToDouble(i.Value.ToString("N7"))}",
                FontSize = 10,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Margin = new Thickness(2),
                Padding = new Thickness(2)
            });
            bigramStatsLB.Items = bigramStats2.Select(i => new ListBoxItem()
            {
                Content = $"{i.Key} - {Convert.ToDouble(i.Value.ToString("N7"))}",
                FontSize = 10,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Margin = new Thickness(2),
                Padding = new Thickness(2)
            });
        }
    }
}