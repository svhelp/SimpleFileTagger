using DAL;

namespace Core.Commands
{
    public abstract class CommandBase<T>
    {
        public CommandBase(TaggerContext context)
        {
            Context = context;
        }

        protected TaggerContext Context { get; }

        public abstract void Run(T model);
    }
}