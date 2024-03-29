﻿using System;
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
            var initaialCommand = string.Join(" ", args.Skip(1));
            var commandModel = ParseCommand(initaialCommand);

            Console.WriteLine(initaialCommand);

            if (commandModel == null)
            {
                return;
            }

            RunCommand(commandModel);
        }
    }
}
