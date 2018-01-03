using System.Collections.Generic;
using System.Threading.Tasks;
using TennisExplorer.Infrastructure;

namespace TennisExplorer.Test.Infrastructure
{
    public class DatabaseInitializer
    {
        private readonly ApplicationConfiguration _applicationConfiguration;

        public DatabaseInitializer(ApplicationConfiguration applicationConfiguration)
        {
            _applicationConfiguration = applicationConfiguration;
        }

        public async Task InitializeAsync()
        {
            await Task.Run(() =>
            {
                using (var db = new LiteDB.LiteDatabase(_applicationConfiguration.DatabaseName))
                {
                    SeedData(db);
                }
            });
        }

        private void SeedData(LiteDB.LiteDatabase database)
        {
            SeedCollection(database, TestData.FavoriteDataCreator.Data);
        }

        private void SeedCollection<TEntity>(LiteDB.LiteDatabase database, ICollection<TEntity> data) where TEntity : class
        {
            var entityTypeName = data.GetType().GetGenericArguments()[0].Name;
            var wasDropped = database.DropCollection(entityTypeName);
            var collection = database.GetCollection<TEntity>();
            collection.InsertBulk(data);
        }
    }
}
