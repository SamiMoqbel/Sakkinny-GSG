using Microsoft.AspNetCore.Mvc;
using Sakkinny.Models.Dtos;
using Sakkinny.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Sakkinny.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class ApartmentController : ControllerBase
	{
		private readonly ApartmentService _apartmentService;
		private readonly ILogger<ApartmentController> _logger;

		public ApartmentController(ApartmentService apartmentService, ILogger<ApartmentController> logger)
		{
			_apartmentService = apartmentService;
			_logger = logger;
		}
		[HttpPost]

		public async Task<IActionResult> AddApartment([FromBody] CreateApartmentDto apartmentDto)
		{
			if (apartmentDto == null)
			{
				return BadRequest("Apartment data is required.");
			}

			var result = await _apartmentService.AddApartment(apartmentDto);
			return Ok(result);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateApartment(int id, [FromBody] UpdateApartmentDto apartmentDto)
		{
			if (apartmentDto == null)
			{
				return BadRequest("Apartment data is required.");
			}

			var updatedApartment = await _apartmentService.UpdateApartment(id, apartmentDto);

			if (updatedApartment == null)
			{
				return NotFound();
			}

			return Ok(updatedApartment);
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteApartment(int id)
		{
			var deletedApartment = await _apartmentService.DeleteApartment(id);

			if (deletedApartment == null)
			{
				return NotFound();
			}

			return Ok(deletedApartment);
		}


		[HttpGet]
		public async Task<ActionResult<IEnumerable<ApartmentDto>>> GetAllApartments([FromQuery] getAllApartmentsDto model)
		{
			var apartments = await _apartmentService.GetAllApartments(model);
			return Ok(new { TotalCount = apartments.Count(), Apartments = apartments });
		}
	}
}
