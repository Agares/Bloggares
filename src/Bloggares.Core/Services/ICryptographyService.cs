namespace Bloggares.Core.Services
{
	public interface ICryptographyService
	{
		byte[] HashPassword(string password);

		byte[] HashPassword(byte[] password);
	}
}