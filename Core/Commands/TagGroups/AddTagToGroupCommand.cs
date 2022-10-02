using AutoMapper;
using Contracts.CommandModels;
using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.TagGroups
{
    public class AddTagToGroupCommand : CommandBase<UpdateGroupCommandModel, CommandResultWith<UpdateGroupTagsCommandResultModel>>
    {
        public AddTagToGroupCommand(TaggerContext context, IMapper mapper) : base(context)
        {
            Mapper = mapper;
        }

        private IMapper Mapper { get; }

        public override CommandResultWith<UpdateGroupTagsCommandResultModel> Run(UpdateGroupCommandModel model)
        {
            var group = Context.TagGroups.FirstOrDefault(g => g.Name == model.GroupName)
                ?? new TagGroupEntity
                    {
                        Name = model.GroupName,
                        Tags = new List<TagEntity>()
                    };

            var existingTagIds = group.Tags.Select(t => t.Id).ToList();
            var tagsToAdd = Context.Tags.Where(t => !existingTagIds.Contains(t.Id) && model.TagIds.Contains(t.Id));

            foreach (var tag in tagsToAdd)
            {
                group.Tags.Add(tag);
            }

            if (group.Id == default)
            {
                Context.TagGroups.Add(group);
            }

            Context.SaveChanges();

            var result = Mapper.Map<UpdateGroupTagsCommandResultModel>(group);

            return GetSuccessfulResult(result);
        }
    }
}
