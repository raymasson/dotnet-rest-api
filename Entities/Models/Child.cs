using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
	[Table("Children")]
	public class Child : IEntity
	{
		[Key]
		[Column("Id")]
		public int Id { get; set; }

		public string Name { get; set; }

        //[Key]
		public int ContactId { get; set; }
	}
}