using AutoMapper;
using Contracts.CommandModels;
using DAL.Entities;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.LocationTags
{
    public class CreateLocationCommand : LocationTagsCommandBase<SimpleNamedModel, CommandResultWith<UpdateLocationCommandResultModel>>
    {
        public CreateLocationCommand(TaggerContext context, IMapper mapper)
            : base(context)
        {
            Mapper = mapper;
        }

        private IMapper Mapper { get; }

        public override CommandResultWith<UpdateLocationCommandResultModel> Run(SimpleNamedModel model)
        {
            var updatedLocation = ProcessLocation(Context, model.Name, location => { });
            var result = Mapper.Map<UpdateLocationCommandResultModel>(updatedLocation);

            return GetSuccessfulResult(result);
        }
    }
}
