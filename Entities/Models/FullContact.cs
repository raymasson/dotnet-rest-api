using System.Collections.Generic;

namespace Entities.Models
{
    public class FullContact
	{
		public int ZipCode { get; set; }

		public string City { get; set; }

		public string FirstName { get; set; }

        public string LastName { get; set; }

		public string Gender { get; set; }

		public List<Child> Children { get; set; }
	}
}