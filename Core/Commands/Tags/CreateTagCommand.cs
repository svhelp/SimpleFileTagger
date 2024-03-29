﻿using Contracts.CommandModels;
using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.Tags
{
    public class CreateTagCommand : CommandBase<SimpleNamedModel, CommandResultWith<Guid>>
    {
        public CreateTagCommand(TaggerContext context)
            : base(context)
        {
        }

        public override CommandResultWith<Guid> Run(SimpleNamedModel model)
        {
            if (Context.Tags.Any(t => t.Name == model.Name))
            {
                return GetErrorResult("Tag with the name already exists.");
            }

            var newTag = new TagEntity
            {
                Name = model.Name,
            };

            Context.Tags.Add(newTag);
            Context.SaveChanges();

            return GetSuccessfulResult(newTag.Id);
        }
    }
}
