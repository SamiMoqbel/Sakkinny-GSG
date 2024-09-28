﻿using Microsoft.AspNetCore.Identity;

namespace Sakkinny.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string FullName { get; set; }
		public string? ProfilePictureUrl { get; set; }
	}
}
