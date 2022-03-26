using System;
using System.IO;
using System.Linq;

namespace IS.Lab7.BL.Helpers
{
    public static class ExampleTextGrabber
    {
        public static string GrabFromExamples()
        {
            var allFiles = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "TextExamples"));
            var path= allFiles[new Random().Next(0, allFiles.Length)];
            if (File.Exists(path))
                return File.ReadAllText(path);
            else throw new FileNotFoundException("Example file not found!");
        }

        public static string GrabSampleText()
        {
            var file = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "SampleText"))
                .FirstOrDefault();
            if (!File.Exists(file)) throw new FileNotFoundException("Example file not found!");
            if (file != null)
                return File.ReadAllText(file);
            throw new FileNotFoundException("Example file not found!");
        }
    }
}