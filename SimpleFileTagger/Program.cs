using SimpleFileTagger.Processors;
using System.Text.RegularExpressions;

namespace SimpleFileTagger
{
    internal class Program
    {
        private static readonly string QuitCommandText = "q";
        private static readonly Regex CommandParser = new Regex("^\"(.+)\" -(r|a|s|d) (.*)$");

        static void Main(string[] args)
        {
            string command;

            Console.WriteLine($"Enter {QuitCommandText} to quit the application.");

            while ((command = Console.ReadLine()) != QuitCommandText)
            {
                var action = CommandParser.Match(command);

                if (!action.Success)
                {
                    continue;
                }

                var path = action.Groups[1].Value;
                var commandType = action.Groups[2].Value;
                var arguments = action.Groups[3].Value.Split().ToList();

                switch (commandType)
                {
                    case "r":
                        {
                            PrintDirectoryInfo(path);
                            break;
                        }
                    case "a":
                        {
                            TagsWriter.AddDirectoryInfo(path, arguments);
                            PrintDirectoryInfo(path);
                            break;
                        }
                    case "s":
                        {
                            TagsWriter.SetDirectoryInfo(path, arguments);
                            PrintDirectoryInfo(path);
                            break;
                        }
                    case "d":
                        {
                            TagsWriter.RemoveDirectoryInfo(path, arguments);
                            PrintDirectoryInfo(path);
                            break;
                        }
                }
            }
        }

        private static void PrintDirectoryInfo(string path)
        {
            var data = TagsReader.GetDirectoryInfo(path);

            foreach(var tag in data.Tags)
            {
                Console.WriteLine(tag.Name);
            }
        }
    }
}