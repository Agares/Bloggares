namespace Bloggares.Core.CQRS
{
	public interface ICommandExecutor
	{
	}

	public interface ICommandExecutor<in TCommand> : ICommandExecutor
		where TCommand : ICommand
	{
		void Execute(TCommand command);
	}
}