using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleFileTagger.Runtime
{
    internal class SingleCommandWorkflow : WorkflowBase
    {
        public void Run(string[] args)
        {
            var initaialCommand = string.Join(" ", args);
            var commandModel = ParseCommand(initaialCommand);

            foreach(var arg in args)
            {
                Console.WriteLine(arg);
            }

            if (commandModel == null)
            {
                return;
            }

            RunCommand(commandModel);
        }
    }
}
