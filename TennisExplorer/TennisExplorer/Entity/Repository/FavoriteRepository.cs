using TennisExplorer.Infrastructure;

namespace TennisExplorer.Entity.Repository
{
    public class FavoriteRepository : BaseLiteDBRepository<Favorite>
    {
        public FavoriteRepository(ApplicationConfiguration applicationConfiguration) : base(applicationConfiguration)
        {
        }
    }
}
