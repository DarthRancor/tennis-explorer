using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TennisExplorer.Entity;
using TennisExplorer.Entity.Repository;
using TennisExplorer.Infrastructure;
using System.Linq;

namespace TennisExplorer.Services
{
	public class FavoriteService
	{
		private readonly FavoriteRepository _favoriteRepository;

		public FavoriteService(FavoriteRepository favoriteRepository)
		{
			_favoriteRepository = favoriteRepository;
		}

		public async Task<IEnumerable<Favorite>> GetAllFavorites()
		{
			var favorites = await Task.Run(() =>
			{
                // order by in service level until litedb v5 is released (sorting not yet supported natively)
				return _favoriteRepository.GetAll().OrderBy(f => f.Name).ToList();
			});

			return favorites;
		}

		public async Task SaveFavorite(Favorite favorite)
		{
			try
			{
				//ValidateEntity(favorite);
				await Task.Run(() =>
				{
					_favoriteRepository.Save(favorite);
				});
			}
			catch (Exception e)
			{
				throw new ServiceException("Ein Fehler ist aufgetreten: " + e.Message);
			}
		}

		public async Task DeleteFavorite(Guid id)
		{
			var paramName = nameof(id) ;
			await Task.Run(() =>
			{
				var wasDeleted = _favoriteRepository.Delete(id);
				if (!wasDeleted)
				{
					throw new ArgumentException($"This favorite does not exist: {id}", paramName);
				}
			});
		}

		//private void ValidateEntity(object instance)
		//{
		//	var validationResults = new List<ValidationResult>();
		//	var isValid = Validator.TryValidateObject(instance, new ValidationContext(instance), validationResults);

		//	if (!isValid)
		//	{
		//		var error = GetMessageFromValidation(validationResults);
		//		throw new ServiceException(error);
		//	}
		//}

		//private string GetMessageFromValidation(ICollection<ValidationResult> validationResults)
		//{
		//	var message = string.Empty;
		//	foreach (var validationError in validationResults)
		//	{
		//		message += $"{Environment.NewLine}{validationError.ErrorMessage}";
		//	}

		//	return message;
		//}
	}
}
