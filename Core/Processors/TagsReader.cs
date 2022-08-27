using Contracts.Models;
using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Processors
{
    public class TagsReader : ProcessorBase
    {
        static public Dictionary<string, TaggerDirectoryInfo> GetDirectoryInfoRecoursively(string path)
        {
            Dictionary<string, TaggerDirectoryInfo> result = new Dictionary<string, TaggerDirectoryInfo>();

            getDirectoryInfoRecoursively(path, result);

            return result;
        }

        static public TaggerDirectoryInfo GetDirectoryInfo(string path)
        {
            return getDirectoryInfo(path);
        }

        private static void getDirectoryInfoRecoursively(string path, Dictionary<string, TaggerDirectoryInfo> result)
        {
            var directoryInfo = getDirectoryInfo(path);
            result.Add(path, directoryInfo);

            var innerDirectories = Directory.GetDirectories(path);

            foreach (var subdir in innerDirectories)
            {
                getDirectoryInfoRecoursively(subdir, result);
            }
        }

        private static TaggerDirectoryInfo getDirectoryInfo(string path)
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
