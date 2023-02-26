using AutoMapper;
using Contracts.CommandModels;
using Contracts.Models.Plain;
using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Commands.LocationTags
{
    public class AddLocationTagCommand : LocationTagsCommandBase<UpdateLocationCommandModel, CommandResultWith<UpdateLocationCommandResultModel>>
    {
        public AddLocationTagCommand(TaggerContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public override CommandResultWith<UpdateLocationCommandResultModel> Run(UpdateLocationCommandModel model)
        {
            var existingTags = Context.Tags.Where(t => model.Tags.Contains(t.Name)).ToList();
            var newTags = model.Tags
                .Where(t => !existingTags.Any(et => et.Name == t))
                .Select(t => new TagEntity
                {
                    Name = t,
                })
                .ToList();
            var tagsToAdd = existingTags.Concat(newTags);

            var affectedLocations = ProcessLocation(model.Path, location => AddTags(location, tagsToAdd), model.IsRecoursive);
            var createdTags = Mapper.Map<List<TagPlainModel>>(newTags);

            return GetSuccessfulResult(new UpdateLocationCommandResultModel {
                Locations = affectedLocations,
                CreatedTags = createdTags,
            });
        }

        private void AddTags(LocationEntity location, IEnumerable<TagEntity> tags)
        {
            var notDuplicatedExistingTags = tags.Where(et => !location.Tags.Contains(et)).ToList();

            foreach (var tag in notDuplicatedExistingTags)
            {
                location.Tags.Add(tag);
            }
        }
    }
}
