using System.ComponentModel.DataAnnotations;


namespace AdvC_Final.Areas.Account.Models
{
	public class Pets
	{
		public int Id { get; set; }
		public string ownerName { get; set; } = string.Empty;
		[Required(ErrorMessage = "Please enter pet's name.")]
		public string petName { get; set; } = string.Empty;
		[Required(ErrorMessage = "Please enter type of pet.")]
		public string petType { get; set; } = string.Empty;
		public string? breed { get; set; } = string.Empty;
		public string? gender { get; set; } = string.Empty;
		public DateTime? birthday { get; set; }
		public int? age { get; set; }
		public string? info { get; set; } = string.Empty;
	}
}
