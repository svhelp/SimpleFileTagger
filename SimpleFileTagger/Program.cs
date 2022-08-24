using DAL;
using Microsoft.EntityFrameworkCore;
using SimpleFileTagger.Models;
using SimpleFileTagger.Processors;
using SimpleFileTagger.Runtime;
using System.Linq;
using System.Text.RegularExpressions;

namespace SimpleFileTagger
{
    internal class Program
    {
        private static readonly string appDataPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FileTagger");

        static void Main(string[] args)
        {
            init();

            if (!args.Any())
            {
                var workflow = new PersistentWorkflow();
                workflow.Run();
                return;
            }

            var mode = args[0];

            switch (mode)
            {
                case "onestep":
                    {
                        new SingleCommandWorkflow().Run(args);
                        break;
                    }
                case "twostep":
                    {
                        new TwoStepWorkflow().Run(args);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Not recognized command");
                        break;
                    }
            }
        }

        private static void init()
        {
            if (Directory.Exists(appDataPath))
            {
                return;
            }

            Directory.CreateDirectory(appDataPath);

            using var context = new TaggerContext();
            context.Database.Migrate();
        }
    }
}