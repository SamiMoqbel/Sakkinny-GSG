﻿using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sakkinny.Models;
using Sakkinny.Models.AuthenticationModels;
using Sakkinny.Services;
using System.Threading.Tasks;

namespace Sakkinny.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly TokenService _tokenService;

		public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TokenService tokenService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequestModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var existingUser = await _userManager.FindByEmailAsync(model.Email);
			if (existingUser != null)
			{
				ModelState.AddModelError("Email", "Email is already in use.");
				return BadRequest(ModelState);
			}

			var user = new ApplicationUser
			{
				FullName = model.FullName,
				Email = model.Email,
				UserName = model.FullName
			};

			var result = await _userManager.CreateAsync(user, model.Password);

			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, model.Role.ToString());
				var response = new RegisterResponseModel
				{
					UserId = user.Id,
					FullName = model.FullName,
					Email = model.Email,
					Message = "User registered successfully.",
					Status = 200,
				};
				return Ok(response);
			}

			return BadRequest(result.Errors);
		}


		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
				return Unauthorized(new { Message = "Invalid email or password." });

			var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
			if (!result.Succeeded)
				return Unauthorized(new { Message = "Invalid email or password." });

			var roles = await _userManager.GetRolesAsync(user);
			var token = _tokenService.CreateToken(user, roles);
			var refreshToken = _tokenService.CreateRefreshToken();

			var response = new LoginResponseModel
			{
				Token = token,
				RefreshToken = refreshToken,
				Email = user.Email,
				Message = "Login successful.",
				Status = 200
			};

			return Ok(response);
		}

		[HttpPost("logout")]
		public async Task<IActionResult> Logout()
		{
			// Sign out the user
			await _signInManager.SignOutAsync();

			return Ok(new { Message = "User logged out successfully.", Status = 200 });
		}

	}
}
