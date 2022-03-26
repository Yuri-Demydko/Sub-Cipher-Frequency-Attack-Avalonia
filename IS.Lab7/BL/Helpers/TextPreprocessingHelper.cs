using System.Text.RegularExpressions;

namespace IS.Lab7.BL.Helpers
{
    public class TextPreprocessingHelper
    {
        public static string PreprocessText(string text)
            => Regex.Replace(text, @"\p{P}", "").ToUpper();
        public static string HardPreprocessText(string text)=>PreprocessText(text)
            .Replace(" ","").Replace("\n","").Replace("\r","");
    }
}