﻿using DAL.Entities;
using SimpleFileTagger.Models;
using SimpleFileTagger.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleFileTagger.Runtime
{
    internal abstract class WorkflowBase
    {
        private readonly Regex CommandParser = new Regex("^\"?(.+)\"? -(r|a|s|d|rr|na|import|root)( (.*))?$");

        protected CommandDTO ParseCommand(string command)
        {
            var action = CommandParser.Match(command);

            if (!action.Success)
            {
                return null;
            }

            return new CommandDTO
            {
                Path = action.Groups[1].Value,
                CommandType = action.Groups[2].Value,
                Tags = action.Groups[4].Value.Split().ToArray()
            };
        }

        protected void RunCommand(CommandDTO command)
        {
            switch (command.CommandType)
            {
                case "r":
                    {
                        PrintDirectoryInfo(command.Path);
                        break;
                    }
                case "rr":
                    {
                        PrintDirectoryInfoRecursively(command.Path);
                        break;
                    }
                case "a":
                    {
                        TagsWriter.AddDirectoryInfo(command.Path, command.Tags);
                        PrintDirectoryInfo(command.Path);
                        break;
                    }
                case "s":
                    {
                        TagsWriter.SetDirectoryInfo(command.Path, command.Tags);
                        PrintDirectoryInfo(command.Path);
                        break;
                    }
                case "d":
                    {
                        TagsWriter.RemoveDirectoryInfo(command.Path, command.Tags);
                        PrintDirectoryInfo(command.Path);
                        break;
                    }
                case "na":
                    {
                        TagsWriter.AddDirectoryNameTags(command.Path);
                        break;
                    }
                case "import":
                    {
                        TagsReader.ImportRootDitectory(command.Path);
                        PrintRootInfoRecursively(command.Path);
                        break;
                    }
                case "root":
                    {
                        PrintRootInfoRecursively(command.Path);
                        break;
                    }
            }
        }
        private static void PrintRootInfoRecursively(string path)
        {
            var data = TagsReader.GetDbRootData(path);

            if (data == null)
            {
                Console.WriteLine("Path does not exist in the DB.");
                return;
            }

            PrintLocationInfo(data.RootLocation, data.Path);
        }

        private static void PrintLocationInfo(LocationEntity location, string? parentPath)
        {
            var currentPath = parentPath ?? string.Empty + location.Name;

            Console.WriteLine(currentPath);

            foreach (var tag in location.Tags)
            {
                Console.WriteLine(tag.Name);
            }

            foreach (var child in location.Children)
            {
                PrintLocationInfo(child, currentPath);
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
