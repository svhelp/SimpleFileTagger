using AutoMapper;
using Contracts.CommandModels;
using Contracts.Models;
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
    public class RemoveLocationTagCommand : LocationTagsCommandBase<UpdateLocationCommandModel, CommandResultWith<UpdateLocationCommandResultModel>>
    {
        public RemoveLocationTagCommand(TaggerContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public override CommandResultWith<UpdateLocationCommandResultModel> Run(UpdateLocationCommandModel model)
        {
            var result = ProcessLocation(model.Path, location => RemoveTags(location, model.Tags), model.IsRecoursive);

            return GetSuccessfulResult(new UpdateLocationCommandResultModel { Locations = result });
        }

        private void RemoveTags(LocationEntity location, string[] tags)
        {
            var tagsToRemove = location.Tags.Where(t => tags.Contains(t.Name));

            foreach (var tagToRemove in tagsToRemove)
            {
                location.Tags.Remove(tagToRemove);
            }
        }
    }
}
