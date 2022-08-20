using SimpleFileTagger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SimpleFileTagger.Processors
{
    internal class TagsReader : ProcessorBase
    {
        static public TaggerDirectoryInfo GetDirectoryInfo(string path)
        {
            var filePath = GetDataFilePath(path);

            if (!File.Exists(filePath))
            {
                return new TaggerDirectoryInfo();
            }

            var fileData = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<TaggerDirectoryInfo>(fileData);
        }
    }
}
