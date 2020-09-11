using System;

namespace Entities.Models
{
	public class ContactParameters : QueryStringParameters
	{
        private const int DefaultMaxAge = 100;

        public int MinAge { get; set; }
		public int MaxAge { get; set; } = DefaultMaxAge;
		public string Gender { get; set; }

		public bool ValidAgeRange => MaxAge > MinAge;
	}
}