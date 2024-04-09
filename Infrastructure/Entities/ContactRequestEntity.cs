using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
	public class ContactRequestEntity
	{
		public int Id { get; set; }
		public  string FullName { get; set; } = null!;
		public string EmailAddress { get; set; } = null!;
		public string Message { get; set; } = null!;
	}

