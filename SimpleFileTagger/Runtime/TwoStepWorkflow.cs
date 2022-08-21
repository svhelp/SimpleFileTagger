using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFileTagger.Runtime
{
    internal class TwoStepWorkflow : WorkflowBase
    {
        public void Run(string[] args)
        {
            var initaialCommand = string.Join(" ", args.Skip(1));
            var commandModel = ParseCommand(initaialCommand);

            Console.WriteLine(initaialCommand);

            if (commandModel == null)
            {
                return;
            }

            Console.WriteLine("Enter tags:");
            var tags = Console.ReadLine();

            commandModel.Tags = tags.Split();

            RunCommand(commandModel);
        }
    }
}
