using AutoMapper;
using Contracts.Models;
using Contracts.QueryModel;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Queries
{
    public class GetTagGroupsQuery : QueryBase<EmptyQueryModel, IEnumerable<TagGroupModel>>
    {
        public GetTagGroupsQuery(IMapper mapper, TaggerContext context) : base(mapper, context)
        {
        }

        public override IEnumerable<TagGroupModel> Run(EmptyQueryModel model)
        {
            var tagGroups = Context.TagGroups.AsQueryable();

            return Mapper.Map<List<TagGroupModel>>(tagGroups);
        }
    }
}
