using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Processors
{
    public abstract class ProcessorBase
    {
        private const string DefaultFileName = ".sft";

        protected static string GetDataFilePath(string directoryPath)
        {
            return Path.Combine(directoryPath, DefaultFileName);
        }
    }
}
