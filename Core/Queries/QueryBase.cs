using AutoMapper;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Queries
{
    public abstract class QueryBase<T, V>
    {
        protected QueryBase(IMapper mapper, TaggerContext context)
        {
            Mapper = mapper;
            Context = context;
        }

        protected IMapper Mapper { get; }
        protected TaggerContext Context { get; }

        public abstract V Run(T model);
    }
}
