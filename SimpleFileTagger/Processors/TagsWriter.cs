using SimpleFileTagger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SimpleFileTagger.Processors
{
    internal class TagsWriter : ProcessorBase
    {
        static public void SetDirectoryInfo(string path, List<string> tags)
        {
            var convertedTags = tags.Select(t => new TagModel { Name = t }).ToList();

            SetDirectoryInfo(path, convertedTags);
        }

        static public void AddDirectoryInfo(string path, List<string> tags)
        {
            var convertedTags = tags.Select(t => new TagModel { Name = t }).ToList();

            AddDirectoryInfo(path, convertedTags);
        }

        static public void RemoveDirectoryInfo(string path, List<string> tags)
        {
            var convertedTags = tags.Select(t => new TagModel { Name = t }).ToList();

            RemoveDirectoryInfo(path, convertedTags);
        }

        static private void SetDirectoryInfo(string path, List<TagModel> tags)
        {
            var filePath = path + DefaultFileName;

            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            var newData = new TaggerDirectoryInfo
            {
                Tags = tags
            };

            var newFileData = JsonSerializer.Serialize(newData);

            File.WriteAllText(filePath, newFileData);
        }

        static private void AddDirectoryInfo(string path, List<TagModel> tags)
        {
            var filePath = path + DefaultFileName;
            var currentTags = new TaggerDirectoryInfo();

            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
            else
            {
                var fileData = File.ReadAllText(filePath);
                currentTags = JsonSerializer.Deserialize<TaggerDirectoryInfo>(fileData);
            }

            currentTags.Tags.AddRange(tags);

            var newFileData = JsonSerializer.Serialize(currentTags);

            File.WriteAllText(filePath, newFileData);
        }

        static private void RemoveDirectoryInfo(string path, List<TagModel> tags)
        {
            var filePath = path + DefaultFileName;

            if (!File.Exists(filePath))
            {
                return;
            }

            var fileData = File.ReadAllText(filePath);
            var currentTags = JsonSerializer.Deserialize<TaggerDirectoryInfo>(fileData);

            currentTags.Tags = currentTags.Tags.Where(currentTag => !tags.Any(t => t.Name == currentTag.Name)).ToList();

            var newFileData = JsonSerializer.Serialize(currentTags);

            File.WriteAllText(filePath, newFileData);
        }
    }
}
