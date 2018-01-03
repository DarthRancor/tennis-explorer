using System;
using System.Collections.Generic;

namespace TennisExplorer.Entity.Repository
{
	public class BaseLiteDBRepository<TEntity> : LiteDB.LiteRepository
	{
		public BaseLiteDBRepository(Infrastructure.ApplicationConfiguration applicationConfiguration)
			: base(applicationConfiguration.DatabaseName)
		{

		}

		public void Save(TEntity entity)
		{
			var createdId = Database.GetCollection<TEntity>().Upsert(entity);
		}

		public IEnumerable<TEntity> GetAll()
		{
			return Database.GetCollection<TEntity>().FindAll();
		}

		public bool Delete(Guid id)
		{
			var wasDeleted = Database.GetCollection<TEntity>().Delete(id);
			return wasDeleted;
		}
	}
}
