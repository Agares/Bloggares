namespace Bloggares.Core.CQRS
{
	public interface ICommandManager
	{
		void Execute<TCommand>(TCommand command)
			where TCommand : ICommand;
	}
}