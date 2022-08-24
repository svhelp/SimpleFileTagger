using DAL;
using DAL.Entities;
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
        static public RootEntity GetDbRootData(string rootPath)
        {
            using var context = new TaggerContext();

            return context.Roots.FirstOrDefault(r => r.Path == rootPath);
        }

        static public void ImportRootDitectory(string rootPath)
        {
            using var context = new TaggerContext();
            var existingTags = context.Tags.AsQueryable().ToList();
            var existingLocations = new List<LocationEntity>();

            var pathMap = new Dictionary<string, LocationEntity>();

            var directoriesData = GetDirectoryInfoRecoursively(rootPath);
            var rootDirectoryData = directoriesData.FirstOrDefault();
            var rootDirectoryPath = rootDirectoryData.Key.Substring(rootPath.Length);

            var rootDirectory = new LocationEntity
            {
                Name = rootDirectoryPath,
                Tags = GetOrCreateTags(existingTags, rootDirectoryData.Value.Tags),
                Children = new List<LocationEntity>()
            };

            pathMap.Add(rootDirectoryPath, rootDirectory);

            var root = new RootEntity
            {
                Path = rootPath,
                RootLocation = rootDirectory
            };

            foreach (KeyValuePair<string, TaggerDirectoryInfo> directory in directoriesData)
            {
                var pathParts = directory.Key.Split("/").ToList();
                var directoryName = pathParts.Last();
                var parentPath = string.Join("/", pathParts.Take(pathParts.Count - 1));

                var locationData = new LocationEntity
                {
                    Name = directoryName,
                    Tags = GetOrCreateTags(existingTags, directory.Value.Tags),
                    Children = new List<LocationEntity>()
                };

                var parent = pathMap[parentPath];
                parent.Children.Add(locationData);

                pathMap.Add(directory.Key, locationData);
            }
            
            context.Roots.Add(root);
        }

        private static List<TagEntity> GetOrCreateTags(List<TagEntity> existingTags, List<TagModel> tags)
        {
            var tagEntities = new List<TagEntity>();

            foreach(var tag in tags)
            {
                var existingTag = existingTags.FirstOrDefault(t => t.Name == tag.Name);

                if (existingTag != null)
                {
                    tagEntities.Add(existingTag);
                    continue;
                }

                var newTagEntity = new TagEntity
                {
                    Name = tag.Name,
                };

                existingTags.Add(newTagEntity);
                tagEntities.Add(newTagEntity);
            }

            return tagEntities;
        }

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

            foreach(var subdir in innerDirectories)
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
