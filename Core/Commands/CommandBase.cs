namespace Core.Commands
{
    public abstract class CommandBase<T>
    {
        public abstract void Run(T model);
    }
}