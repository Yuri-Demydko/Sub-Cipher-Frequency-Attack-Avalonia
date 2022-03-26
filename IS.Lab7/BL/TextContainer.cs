using System.Linq;
using IS.Lab7.BL.SymbolReplacement;

namespace IS.Lab7.BL
{
    public class TextContainer
    {
        public string OriginalText { get; set; }
        public SymbolReplacementKeyDto Key { get; set; }

        //public string? ProcessedText => OriginalText.Select(i => Key.replacements[i]).ToString();
    }
}