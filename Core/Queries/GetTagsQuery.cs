using AutoMapper;
using Contracts.Models;
using Core.Processors;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Queries
{
    public class GetTagsQuery : QueryBase<Guid?, IEnumerable<TagModel>>
    {
        public GetTagsQuery(TaggerContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        private TaggerContext Context { get; }
        private IMapper Mapper { get; }

        public override IEnumerable<TagModel> Run(Guid? id)
        {
            var tags = id == null
                ? Context.Tags.AsQueryable()
                : Context.Tags.Where(t => t.Id == id);

            var result = Mapper.Map<List<TagModel>>(tags);

            return result;
        }
    }
}
