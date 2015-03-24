using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Anlis.Core
{
    
    public class RandomPhraseService
    {
        private static readonly Random _random = new Random((int)DateTime.Now.Ticks);

        public static string[] GetAllPhrasesFromResource(Assembly resourceAssembly, string resourceFileName)
        {
            var fileText = GetTextFromResource(resourceAssembly, resourceFileName);
            return GetLines(fileText);
        }

        public static string GetRandomPhraseFromResource(
            Assembly resourceAssembly = null, 
            string resourceFileName = "Data.TheHistorian.txt"
            )
        {
            var fileText = GetTextFromResource(resourceAssembly, resourceFileName);

            return GetRandomPhraseFromText(fileText);
        }

        private static string GetTextFromResource(Assembly resourceAssembly, string resourceFileName)
        {
            var assembly = resourceAssembly ?? Assembly.GetExecutingAssembly();
            //var names = assembly.GetManifestResourceNames();
            var resourceName = assembly.GetName().Name + "." + resourceFileName;

            string fileText = null;
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                fileText = reader.ReadToEnd();
            }
            return fileText;
        }

        public static string GetRandomPhraseFromText(string text)
        {
            char separator = '.';
            var phrases = GetLines(text);
            var randomPhrase = phrases[_random.Next(phrases.Count() - 1)].Trim() + separator;
            return randomPhrase;
        }

        private static string[] GetLines(string text, char separator = '.')
        {
            return text.Replace("\r\n", " ").Split(separator);
        }
    }
}