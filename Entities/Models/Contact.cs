using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
	[Table("Contacts")]
	public class Contact : IEntity
	{
		[Key]
		[Column("Id")]
		public int Id { get; set; }

		[Required(ErrorMessage = "First name is required")]
		[StringLength(60, ErrorMessage = "First name can't be longer than 60 characters")]
		public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
		[StringLength(60, ErrorMessage = "Last name can't be longer than 60 characters")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Age is required")]
		public int Age { get; set; }
	}
}