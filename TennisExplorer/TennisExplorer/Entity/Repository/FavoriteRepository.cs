using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TennisExplorer.Entity.Repository
{
	public class FavoriteRepository
	{
		private readonly TennisMatchSpyDbContext _context;

		public FavoriteRepository(TennisMatchSpyDbContext context)
		{
			_context = context;
		}

		public async Task<List<Favorite>> GetAll()
		{
			return await _context.Favorites.ToListAsync();
		}

		public async Task Add(Favorite favorite)
		{
			await _context.Favorites.AddAsync(favorite);
			await _context.SaveChangesAsync();
		}

		public async Task Change(Favorite favorite)
		{
			//var state = _context.Entry(favorite).State;
			//_context.Entry(favorite).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async Task<Favorite> Find(int id)
		{
			return await _context.Favorites.FindAsync(id);
		}

		//public async Task<Favorite> GetByName(string name)
		//{
		//	return await _context.Favorites.FirstOrDefaultAsync(f => f.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
		//}

		public async Task Delete(int id)
		{
			var entity = await _context.Favorites.FindAsync(id);
			if (entity == null)
			{
				throw new ArgumentException("Dieser Favorit existiert nicht");
			}

			_context.Remove(entity);
			await _context.SaveChangesAsync();
		}
	}
}
