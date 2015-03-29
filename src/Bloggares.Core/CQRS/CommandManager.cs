using System.Collections.Generic;
using System.Linq;

namespace Bloggares.Core.CQRS
{
	public class CommandManager : ICommandManager
	{
		private readonly IEnumerable<ICommandExecutor> executors;

		public CommandManager(IEnumerable<ICommandExecutor> executors)
		{
			this.executors = executors;
		}

		public void Execute<TCommand>(TCommand command)
			where TCommand : ICommand
		{
			var executor = (ICommandExecutor<TCommand>)executors.Single
				(
					x => x.GetType()
						.GetInterfaces()
						.Contains(typeof(ICommandExecutor<TCommand>))
				);	// todo maybe just use serviceCollection here?

			executor.Execute(command);
		}
	}
}