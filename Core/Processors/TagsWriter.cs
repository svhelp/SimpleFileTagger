using Contracts.Models;
using Contracts.Models.Legacy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Processors
{
    public class TagsWriter : ProcessorBase
    {
        static public void SetDirectoryInfo(string path, IEnumerable<string> tags)
        {
            var convertedTags = tags.Select(t => new SimpleModel { Name = t }).ToList();

            SetDirectoryInfo(path, convertedTags);
        }

        static public void AddDirectoryInfo(string path, IEnumerable<string> tags)
        {
            var convertedTags = tags.Select(t => new SimpleModel { Name = t }).ToList();

            AddDirectoryInfo(path, convertedTags);
        }

        static public void RemoveDirectoryInfo(string path, IEnumerable<string> tags)
        {
            var convertedTags = tags.Select(t => new SimpleModel { Name = t }).ToList();

            RemoveDirectoryInfo(path, convertedTags);
        }

        static public void AddDirectoryNameTags(string path)
        {
            var innerDirectories = Directory.GetDirectories(path);

            foreach (var innerDirectory in innerDirectories)
            {
                var name = innerDirectory.Split("/").Last();
                var tag = new SimpleModel
                {
                    Name = name,
                };

                SetDirectoryInfo(name, new List<SimpleModel> { tag });
            }
        }


        static private void SetDirectoryInfo(string path, List<SimpleModel> tags)
        {
            ProcessDirectoryInfo(path, (currentTags) => new TaggerDirectoryInfo
            {
                Tags = tags
            });
        }

        static private void AddDirectoryInfo(string path, List<SimpleModel> tags)
        {
            ProcessDirectoryInfo(path, (currentTags) => new TaggerDirectoryInfo
            {
                Tags = currentTags.Tags.Concat(tags).ToList()
            });
        }

        static private void RemoveDirectoryInfo(string path, List<SimpleModel> tags)
        {
            ProcessDirectoryInfo(path, (currentTags) => new TaggerDirectoryInfo
            {
                Tags = currentTags.Tags.Where(currentTag => !tags.Any(t => t.Name == currentTag.Name)).ToList()
            });
        }

        private static void ProcessDirectoryInfo(string path, Func<TaggerDirectoryInfo, TaggerDirectoryInfo> processor)
        {
            var filePath = GetDataFilePath(path);

            using FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            using var streamReader = new StreamReader(stream);
            var fileData = streamReader.ReadToEnd();
            var currentTags = fileData == string.Empty
                ? new TaggerDirectoryInfo()
                : JsonSerializer.Deserialize<TaggerDirectoryInfo>(fileData);

            var updatedData = processor(currentTags);

            var newFileData = JsonSerializer.Serialize(updatedData);

            stream.SetLength(0);
            using var streamWriter = new StreamWriter(stream);
            streamWriter.WriteLine(newFileData);
        }
    }
}
