using Microsoft.AspNetCore.Mvc;
using Sakkinny.Models.Dtos;
using Sakkinny.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

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
        public async Task<ActionResult<IEnumerable<ApartmentDto>>> GetAllApartments()
        {
            var apartments = await _apartmentService.GetAllApartments();
            return Ok(apartments);
        }

        [HttpPost]
public async Task<IActionResult> RentApartment([FromBody] RentApartmentDto rentApartmentDto)
{
    if (rentApartmentDto == null)
    {
        return BadRequest("Rental data is required.");
    }

    // rent the apartment by Muhnnad
    var result = await _apartmentService.RentApartment(rentApartmentDto);

    if (!result.IsSuccess)
    {
        return BadRequest(result.Message);
    }

    // Check if the apartment is now full
    var apartment = await _apartmentService.GetApartmentById(rentApartmentDto.ApartmentId);
    if (apartment != null && apartment.RoomsAvailable == 0)
    {
        apartment.IsRented = true; // Mark the apartment as full
        await _apartmentService.UpdateApartmentEntity(apartment); // Ensure to update the apartment
    }

    return Ok(result.Message);
}


    }
}
