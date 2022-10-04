using Contracts.Models;
using DAL.Entities;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Processors;
using Contracts.Models.Legacy;

namespace Core.Importers
{
    public class LegacyDataImporter
    {
        public LegacyDataImporter(TaggerContext context)
        {
            Context = context;
        }

        private TaggerContext Context { get; }

        public void ImportRootDitectory(string rootPath)
        {
            var existingTags = Context.Tags.AsQueryable().ToList();

            var pathMap = new Dictionary<string, LocationEntity>();

            var directoriesData = TagsReader.GetDirectoryInfoRecoursively(rootPath);

            var rootDirectoryData = directoriesData.FirstOrDefault();
            var rootDirectory = GetDirectoryDbData(rootDirectoryData, existingTags, out string _);

            pathMap.Add(rootDirectoryData.Key, rootDirectory);

            var root = new RootEntity
            {
                Path = rootPath,
                RootLocation = rootDirectory
            };

            foreach (KeyValuePair<string, TaggerDirectoryInfo> directory in directoriesData.Skip(1))
            {
                var locationData = GetDirectoryDbData(directory, existingTags, out string parentPath);

                var parent = pathMap[parentPath];
                parent.Children.Add(locationData);

                pathMap.Add(directory.Key, locationData);
            }

            Context.Roots.Add(root);
            Context.SaveChanges();
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
                Path = directory.Key,
                Tags = GetOrCreateTags(existingTags, directory.Value.Tags),
                Children = new List<LocationEntity>()
            };
        }

        private static List<TagEntity> GetOrCreateTags(List<TagEntity> existingTags, List<SimpleModel> tags)
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
    }
}
