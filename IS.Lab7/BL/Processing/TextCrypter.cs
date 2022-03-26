using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using IS.Lab7.BL.Helpers;
using IS.Lab7.BL.SymbolReplacement;

namespace IS.Lab7.BL.Processing
{
    public class TextCrypter
    {
        private TextContainer container;

        public string OriginalText => container.OriginalText;

        public TextCrypter(FileInfo textFile)
        {
            container = new TextContainer
            {
                OriginalText = Regex.Replace(File.ReadAllText(textFile.FullName), @"\p{P}", "").ToUpper(),
                Key = SymbolReplacementKeyFactory.Generate()
            };
        }

       
        public TextCrypter(string originalText)
        {
            container = new TextContainer
            {
                OriginalText = TextPreprocessingHelper.PreprocessText(originalText),
                Key = SymbolReplacementKeyFactory.Generate()
            };
        }

        public string Process() => Process(container.Key, container.OriginalText);
        public string Process(SymbolReplacementKeyDto symbolReplacements, string text)
        {
            return string.Join("",TextPreprocessingHelper.PreprocessText(text).Select(i =>
                char.IsLetter(i)&&symbolReplacements.replacements.ContainsKey(i) ? symbolReplacements.replacements[i] : i))
                   ?? throw new InvalidOperationException("Null result of processing!");
        }

        public bool Validate(string value)
            => OriginalText == value;

    }
}