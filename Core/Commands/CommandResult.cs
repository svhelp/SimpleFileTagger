using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands
{
    public class CommandResult
    {
        public CommandResult()
        {
            IsSuccessful = true;
        }

        public CommandResult(string message)
        {
            Message = message;
        }

        public bool IsSuccessful { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
