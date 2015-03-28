using System.Security.Cryptography;
using System.Text;

namespace Bloggares.Services
{
	public class CryptographyService
	{
		private SHA512 hasher = SHA512.Create();

		// todo hashing shall be done in other service
		public byte[] HashPassword(string password)
		{
			byte[] passwordBytes = Encoding.UTF32.GetBytes(password);

			return HashPassword(passwordBytes);
		}

		public byte[] HashPassword(byte[] password)
		{
			return hasher.ComputeHash(password);
		}
	}
}