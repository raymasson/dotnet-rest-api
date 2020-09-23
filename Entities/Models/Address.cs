using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Addresses")]
	public class Address
	{
		public int ZipCode { get; set; }

		public string City { get; set; }

        [Key]
		public int ContactId { get; set; }
	}
}