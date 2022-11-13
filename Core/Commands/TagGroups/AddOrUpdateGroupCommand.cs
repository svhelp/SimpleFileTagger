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
    public class AddOrUpdateGroupCommand : CommandBase<UpdateGroupCommandModel, CommandResultWith<UpdateGroupTagsCommandResultModel>>
    {
        public AddOrUpdateGroupCommand(TaggerContext context, IMapper mapper) : base(context)
        {
            Mapper = mapper;
        }

        private IMapper Mapper { get; }

        public override CommandResultWith<UpdateGroupTagsCommandResultModel> Run(UpdateGroupCommandModel model)
        {
            var group = model.Id != default
                ? Context.TagGroups.FirstOrDefault(g => g.Id == model.Id)
                : new TagGroupEntity
                    {
                        Tags = new List<TagEntity>()
                    };

            group.Name = model.Name;
            group.IsRequired = model.IsRequired;

            var existingTagIds = group.Tags.Select(t => t.Id).ToList();
            var tagsToAdd = Context.Tags.Where(t => !existingTagIds.Contains(t.Id) && model.TagIds.Contains(t.Id));
            var tagsToRemove = group.Tags.Where(t => !model.TagIds.Contains(t.Id)).ToList();

            foreach (var tag in tagsToAdd)
            {
                group.Tags.Add(tag);
            }

            foreach (var tag in tagsToRemove)
            {
                group.Tags.Remove(tag);
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
