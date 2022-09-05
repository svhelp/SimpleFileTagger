using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Queries
{
    public abstract class QueryBase<T, V>
    {
        public abstract V Run(T model);
    }
}
