using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace Bloggares.Database
{
	// todo DatabaseUpgradeController?
	public class MigrationController : IMigrationController
	{
		private readonly Assembly assemblyWithMigrations;
		private readonly IDbConnection connection;
		private readonly MigrationDAO migrationDAO;

		public MigrationController(IDbConnection connection, MigrationDAO migrationDAO, Assembly assemblyWithMigrations = null)
		{
			Contract.Requires(connection != null);
			Contract.Ensures(this.connection != null);
			Contract.Ensures(this.assemblyWithMigrations != null);

			if (assemblyWithMigrations == null)
			{
				assemblyWithMigrations = Assembly.GetExecutingAssembly();
			}

			this.assemblyWithMigrations = assemblyWithMigrations;
			this.connection = connection;
			this.migrationDAO = migrationDAO;
		}

		// todo DowngradeDatabase

		public void UpgradeDatabase()
		{
			// todo make this method shorter, it certainly violates SRP
			migrationDAO.EnsureMigrationsTableExists();

			var executedMigrationsIds = migrationDAO.FindAlreadyExecutedMigrationIds();

			var migrationTypes = assemblyWithMigrations	// TODO just use DI
				.GetTypes()
				.Where(x => x.GetInterfaces().Where(y => y == typeof(IMigration)).Any()) // todo move to extension method(s)
				.Where(x => !x.IsAbstract);

			var migrations = new List<IMigration>();

			foreach (var type in migrationTypes)
			{
				migrations.Add((IMigration)Activator.CreateInstance(type));
			}

			var migrationsToExecute = migrations
				.Where(x => !executedMigrationsIds.Contains(x.Id))
				.OrderBy(x => x.Id);

			foreach (var x in migrationsToExecute)
			{
				var transaction = connection.BeginTransaction(IsolationLevel.Snapshot);

				try
				{
					x.Execute(connection);
					migrationDAO.MarkAsExecuted(x.Id);

					transaction.Commit();
				}
				catch (Exception)
				{
					transaction.Rollback();
					throw;
				}
				finally
				{
					transaction.Dispose();
				}
			}
		}
	}
}