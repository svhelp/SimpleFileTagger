using Contracts.CommandModels;
using Contracts.Models;
using Core.Commands;
using Core.Commands.LocationTags;
using Core.Importers;
using Core.Processors;
using Core.Queries;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleFileTagger.Runtime
{
    internal abstract class WorkflowBase
    {
        private readonly Regex CommandParser = new Regex("^\"?(.+?)\"? -(r|a|s|d|rr|na|import|add|set|delete)( (.*))?$");
        private string[] LegacyCommands = new string[] { "r", "a", "s", "d", "rr", "na" };

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
            if (LegacyCommands.Contains(command.CommandType))
            {
                RunLegacyCommand(command);
                return;
            }

            RunDbCommand(command);
        }

        protected void RunLegacyCommand(CommandDTO command)
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
            }
        }

        protected void RunDbCommand(CommandDTO command)
        {
            using var context = new TaggerContext();

            switch (command.CommandType)
            {
                case "import":
                    {
                        var legacyDataImporter = new LegacyDataImporter(context);
                        legacyDataImporter.ImportRootDitectory(command.Path);
                        break;
                    }
                case "add":
                    {
                        var action = new AddLocationTagCommand(context);
                        var model = new UpdateTagsCommandModel { Path = command.Path, Tags = command.Tags };
                        action.Run(model);
                        break;
                    }
                case "set":
                    {
                        var action = new SetLocationTagsCommand(context);
                        var model = new UpdateTagsCommandModel { Path = command.Path, Tags = command.Tags };
                        action.Run(model);
                        break;
                    }
                case "delete":
                    {
                        var action = new RemoveLocationTagCommand(context);
                        var model = new UpdateTagsCommandModel { Path = command.Path, Tags = command.Tags };
                        action.Run(model);
                        break;
                    }
            }
        }

        private static void PrintLocationInfo(TaggerDirectoryInfo directoryInfo)
        {
            Console.WriteLine(directoryInfo.Path);

            foreach(var tag in directoryInfo.Tags)
            {
                Console.WriteLine(tag.Name);
            }

            foreach (var child in directoryInfo.Children)
            {
                PrintLocationInfo(child);
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
