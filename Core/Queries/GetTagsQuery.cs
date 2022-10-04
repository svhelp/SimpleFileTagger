using AutoMapper;
using Contracts.Models;
using Contracts.Models.Plain;
using Core.Processors;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Queries
{
    public class GetTagsQuery : QueryBase<Guid?, IEnumerable<TagPlainModel>>
    {
        public GetTagsQuery(TaggerContext context, IMapper mapper)
            : base(mapper, context)
        {
        }

        public override IEnumerable<TagPlainModel> Run(Guid? id)
        {
            var tags = id == null
                ? Context.Tags.AsQueryable()
                : Context.Tags.Where(t => t.Id == id);

            var result = Mapper.Map<List<TagPlainModel>>(tags);

            return result;
        }
    }
}
