using AutoMapper;
using Contracts.CommandModels;
using DAL.Entities;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Commands.LocationTags;
using Contracts.Models.Plain;
using System.IO;

namespace Core.Commands.Locations
{
    public class CreateLocationCommand : LocationTagsCommandBase<CreateLocationCommandModel, CommandResultWith<UpdateLocationCommandResultModel>>
    {
        public CreateLocationCommand(TaggerContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public override CommandResultWith<UpdateLocationCommandResultModel> Run(CreateLocationCommandModel model)
        {
            var directory = new DirectoryInfo(model.Path);

            if (!directory.Exists)
            {
                return GetErrorResult("Directory does not exist.");
            }
            
            var result = ProcessLocation(model.Path, location => { }, model.IsRecoursive);

            return GetSuccessfulResult(new UpdateLocationCommandResultModel { Locations = result });
        }
    }
}
