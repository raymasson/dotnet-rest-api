using System;

namespace Entities.Models
{
	public class ContactParameters : QueryStringParameters
	{
        private const uint DefaultMaxAge = 100;

        public uint MinAge { get; set; }
		public uint MaxAge { get; set; } = DefaultMaxAge;

		public bool ValidAgeRange => MaxAge > MinAge;
	}
}