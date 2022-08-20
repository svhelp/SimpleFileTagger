using SimpleFileTagger.Models;
using SimpleFileTagger.Processors;
using System.Text.RegularExpressions;

namespace SimpleFileTagger
{
    internal class Program
    {
        private static readonly string QuitCommandText = "q";
        private static readonly Regex CommandParser = new Regex("^\"(.+)\" -(r|a|s|d|rr)( (.*))?$");

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
                var arguments = action.Groups[4].Value.Split().ToList();

                switch (commandType)
                {
                    case "r":
                        {
                            PrintDirectoryInfo(path);
                            break;
                        }
                    case "rr":
                        {
                            PrintDirectoryInfoRecursively(path);
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
        private static void PrintDirectoryInfoRecursively(string path)
        {
            var data = TagsReader.GetDirectoryInfoRecoursively(path);

            foreach (var dir in data.Keys)
            {
                Console.WriteLine(dir);
                PrintDirectoryInfo(data[dir]);
            }
        }

        private static void PrintDirectoryInfo(string path)
        {
            var data = TagsReader.GetDirectoryInfo(path);
            PrintDirectoryInfo(data);
        }

        private static void PrintDirectoryInfo(TaggerDirectoryInfo dirInfo)
        {
            foreach (var tag in dirInfo.Tags)
            {
                Console.WriteLine(tag.Name);
            }
        }
    }

}