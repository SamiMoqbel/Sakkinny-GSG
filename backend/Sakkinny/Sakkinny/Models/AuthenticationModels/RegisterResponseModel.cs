﻿namespace Sakkinny.Models.AuthenticationModels
{
	public class RegisterResponseModel
	{
		public string UserId { get; set; }
		public string Email { get; set; }
		public string FullName { get; set; }
		public string Message { get; set; }
		public int Status { get; set; }
	}
}
