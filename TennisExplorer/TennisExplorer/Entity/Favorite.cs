using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TennisExplorer.Entity
{
	[Table("Favorites")]
	public class Favorite
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[MaxLength(255)]
		[Required]
		public string Name { get; set; }
	}
}
