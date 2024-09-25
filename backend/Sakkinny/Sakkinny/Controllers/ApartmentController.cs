using Microsoft.AspNetCore.Mvc;
using Sakkinny.Models.Dtos;
using Sakkinny.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Sakkinny.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

            var updated = await _apartmentService.UpdateApartment(id, apartmentDto);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApartment(int id)
        {
            var deleted = await _apartmentService.DeleteApartment(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApartmentDto>>> GetAllApartments()
        {
            var apartments = await _apartmentService.GetAllApartments();
            return Ok(apartments);
        }
    }
}
