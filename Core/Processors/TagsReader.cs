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
        static public void PrintRootInfoRecursively(string rootPath)
        {
            using var context = new TaggerContext();

            var root = context.Roots.FirstOrDefault(r => r.Path == rootPath);

            if (root == null)
            {
                Console.WriteLine("Path does not exist in the DB.");
                return;
            }

            PrintLocationInfo(root.RootLocation, string.Empty);
        }


        private static void PrintLocationInfo(LocationEntity location, string? parentPath)
        {
            var currentPath = (parentPath ?? string.Empty) + "\\" + location.Name;

            Console.WriteLine(currentPath);

            foreach (var tag in location.Tags)
            {
                Console.WriteLine(tag.Name);
            }

            foreach (var child in location.Children)
            {
                PrintLocationInfo(child, currentPath);
            }
        }

        static public void ImportRootDitectory(string rootPath)
        {
            using var context = new TaggerContext();
            var existingTags = context.Tags.AsQueryable().ToList();
            var existingLocations = new List<LocationEntity>();

            var pathMap = new Dictionary<string, LocationEntity>();

            var directoriesData = GetDirectoryInfoRecoursively(rootPath);

            var rootDirectoryData = directoriesData.FirstOrDefault();
            var rootDirectory = GetDirectoryDbData(rootDirectoryData, existingTags, out string _);

            pathMap.Add(rootDirectoryData.Key, rootDirectory);

            var root = new RootEntity
            {
                Path = rootPath,
                RootLocation = rootDirectory
            };

            //rootDirectory.Root = root;

            foreach (KeyValuePair<string, TaggerDirectoryInfo> directory in directoriesData.Skip(1))
            {
                var locationData = GetDirectoryDbData(directory, existingTags, out string parentPath);

                var parent = pathMap[parentPath];
                parent.Children.Add(locationData);
                //locationData.Parent = parent;

                pathMap.Add(directory.Key, locationData);
            }

            context.Roots.Add(root);
            context.SaveChanges();
        }

        private static LocationEntity GetDirectoryDbData(
            KeyValuePair<string, TaggerDirectoryInfo> directory,
            List<TagEntity> existingTags,
            out string parentPath)
        {
            var directoryName = Path.GetFileName(directory.Key);
            parentPath = Path.GetDirectoryName(directory.Key);

            return new LocationEntity
            {
                Name = directoryName,
                Tags = GetOrCreateTags(existingTags, directory.Value.Tags),
                Children = new List<LocationEntity>()
            };
        }

        private static List<TagEntity> GetOrCreateTags(List<TagEntity> existingTags, List<TagModel> tags)
        {
            var tagEntities = new List<TagEntity>();

            foreach (var tag in tags)
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
