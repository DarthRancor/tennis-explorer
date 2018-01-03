using LiteDB;
using System;

namespace TennisExplorer.Entity
{
	public class Favorite
	{
		[BsonId]
		public Guid Id { get; set; }

		public string Name { get; set; }
	}
}
