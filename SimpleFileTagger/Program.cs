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
            if (args.Any())
            {
                new SingleCommandWorkflow().Run(args);
                return;
            }

            var workflow = new PersistentWorkflow();
            workflow.Run();
        }
    }
}