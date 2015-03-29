using Bloggares.Core.Commands;
using Bloggares.Core.CQRS;
using Bloggares.Core.Services;
using Bloggares.Core.Services.DAL;

namespace Bloggares.Core.CommandExecutors
{
	public class UserCreateCommandExecutor : ICommandExecutor<UserCreateCommand>
	{
		private readonly ICryptographyService cryptographyService;
		private readonly IUserDAL userDAL;

		public UserCreateCommandExecutor(ICryptographyService cryptographyService, IUserDAL userDAL)
		{
			this.cryptographyService = cryptographyService;
			this.userDAL = userDAL;
		}

		public void Execute(UserCreateCommand command)
		{
			var passwordHash = cryptographyService.HashPassword(command.Password);

			userDAL.Create(command.Username, passwordHash, command.AccessLevel);
		}
	}
}