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

        protected IMapper Mapper { get; }

        protected ProcessorBase()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CoreMapperProfile>();
            });

            Mapper = mapperConfig.CreateMapper();
        }

        protected static string GetDataFilePath(string directoryPath)
        {
            return Path.Combine(directoryPath, DefaultFileName);
        }
    }
}
