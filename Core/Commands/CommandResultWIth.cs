using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands
{
    public class CommandResultWith<T> : CommandResult
    {
        public CommandResultWith()
        {

        }

        public CommandResultWith(T data)
            : base()
        {
            Data = data;
        }

        public CommandResultWith(string message)
            : base(message)
        {

        }

        public T? Data { get; set; }
    }
}
