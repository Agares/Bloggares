using System.Data;

namespace Bloggares.Database
{
    public interface IMigration
    {
		int Id { get; }

		void Execute(IDbConnection connection);

		void Revert(IDbConnection connection);
    }
}