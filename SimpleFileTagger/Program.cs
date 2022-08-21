using SimpleFileTagger.Models;
using SimpleFileTagger.Processors;
using SimpleFileTagger.Runtime;
using System.Linq;
using System.Text.RegularExpressions;

namespace SimpleFileTagger
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
                        new SingleCommandWorkflow().Run(args);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Not recognized command");
                        break;
                    }
            }
        }
    }
}