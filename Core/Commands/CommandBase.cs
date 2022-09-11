using DAL;

namespace Core.Commands
{
    public abstract class CommandBase<T, V>
        where V : CommandResult, new()
    {
        public CommandBase(TaggerContext context)
        {
            Context = context;
        }

        protected TaggerContext Context { get; }

        public abstract V Run(T model);

        protected CommandResult GetSuccessfulResult()
        {
            return new CommandResult
            {
                IsSuccessful = true,
            };
        }

        protected CommandResultWith<U> GetSuccessfulResult<U>(U data)
        {
            return new CommandResultWith<U>
            {
                IsSuccessful = true,
                Data = data,
            };
        }

        protected V GetErrorResult(string message)
        {
            return new V
            {
                Message = message
            };
        }
    }
}