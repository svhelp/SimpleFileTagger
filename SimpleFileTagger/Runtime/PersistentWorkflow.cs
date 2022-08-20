using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleFileTagger.Runtime
{
    internal class PersistentWorkflow : WorkflowBase
    {
        private static readonly string QuitCommandText = "q";

        public void Run()
        {
            string command;

            Console.WriteLine($"Enter {QuitCommandText} to quit the application.");

            while ((command = Console.ReadLine()) != QuitCommandText)
            {
                var commandModel = ParseCommand(command);

                if (commandModel == null)
                {
                    continue;
                }

                RunCommand(commandModel);
            }
        }
    }
}
