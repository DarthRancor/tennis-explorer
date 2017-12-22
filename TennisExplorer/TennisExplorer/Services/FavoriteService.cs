using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TennisExplorer.Entity;
using TennisExplorer.Entity.Repository;
using TennisExplorer.Infrastructure;

namespace TennisExplorer.Services
{
	public class FavoriteService
	{
		private readonly FavoriteRepository _favoriteRepository;

		public FavoriteService(FavoriteRepository favoriteRepository)
		{
			_favoriteRepository = favoriteRepository;
		}

		public async Task<List<Favorite>> GetAllFavorites()
		{
			return await _favoriteRepository.GetAll();
		}

		public async Task CreateFavorite(Favorite favorite)
		{
			try
			{
				ValidateEntity(favorite);
				await _favoriteRepository.Add(favorite);
			}
			catch (Exception e)
			{
				HandleSqlException(e);
				throw new ServiceException("Ein Fehler ist aufgetreten: " + e.Message);
			}
		}

		private void HandleSqlException(Exception e)
		{
			if (e.InnerException is SqliteException)
			{
				throw new ServiceException("Ein Fehler ist aufgetreten: " + e.InnerException.Message);
			}
		}

		public async Task ChangeFavorite(Favorite favorite)
		{
			try
			{
				ValidateEntity(favorite);
				Favorite existingFavorite = await _favoriteRepository.Find(favorite.Id);
				if (favorite == null)
				{
					throw new ArgumentException("Dieser Eintrag existiert nicht");
				}

				existingFavorite.Name = favorite.Name;
				await _favoriteRepository.Change(favorite);
			}
			catch (Exception e)
			{
				throw new ServiceException("Ein Fehler ist aufgetreten: " + e.Message);
			}
		}

		public async Task DeleteFavorite(int id)
		{
			await _favoriteRepository.Delete(id);
		}

		private void ValidateEntity(object instance)
		{
			var validationResults = new List<ValidationResult>();
			var isValid = Validator.TryValidateObject(instance, new ValidationContext(instance), validationResults);

			if (!isValid)
			{
				var error = GetMessageFromValidation(validationResults);
				throw new ServiceException(error);
			}
		}

		private string GetMessageFromValidation(ICollection<ValidationResult> validationResults)
		{
			var message = string.Empty;
			foreach (var validationError in validationResults)
			{
				message += $"{Environment.NewLine}{validationError.ErrorMessage}";
			}

			return message;
		}
	}
}
