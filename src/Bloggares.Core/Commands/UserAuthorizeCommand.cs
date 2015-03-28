namespace Bloggares.Core.Commands
{
	public class UserAuthorizeCommand
	{
		public string Username { get; }

		public string Password { get; }

		public UserAuthorizeCommand(string username, string password)
		{
			Username = username;
			Password = password;
		}
	}
}