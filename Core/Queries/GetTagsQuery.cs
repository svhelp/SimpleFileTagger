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
    public class GetTagsQuery
    {
        public GetTagsQuery()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CoreMapperProfile>();
            });

            Mapper = mapperConfig.CreateMapper();
        }

        private IMapper Mapper { get; }

        public IEnumerable<TagModel> Run(Guid? id)
        {
            using var context = new TaggerContext();

            var tags = id == null
                ? context.Tags.AsQueryable()
                : context.Tags.Where(t => t.Id == id);

            var result = Mapper.Map<List<TagModel>>(tags);

            return result;
        }
    }
}
