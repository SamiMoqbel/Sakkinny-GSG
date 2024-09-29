using Microsoft.AspNetCore.Mvc;
using Sakkinny.Models;
using Sakkinny.Models.Dtos;
using Sakkinny.Services;

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
        public async Task<IActionResult> AddApartment(CreateApartmentDto apartmentDto)
        {
            if (apartmentDto == null)
            {
                return BadRequest("Apartment data is required.");
            }

            var result = await _apartmentService.AddApartment(apartmentDto);
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApartment(int id, [FromForm] UpdateApartmentDto apartmentDto)
        {
            if (apartmentDto == null)
            {
                return BadRequest("Apartment data is required.");
            }

            var updatedApartment = await _apartmentService.UpdateApartment(id, apartmentDto);

            if (updatedApartment == null)
            {
                return NotFound($"Apartment with ID {id} not found.");
            }

            return Ok(updatedApartment);
        }

        // Delete Apartment
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApartment(int id)
        {
            var deletedApartment = await _apartmentService.DeleteApartment(id);

            if (deletedApartment == null)
            {
                return NotFound($"Apartment with ID {id} not found.");
            }

            return Ok(deletedApartment);
        }
        [HttpGet("names")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllApartmentNames()
        {
            _logger.LogInformation("Retrieving all apartment names.");

            try
            {
                var apartmentNames = await _apartmentService.GetAllApartmentNames();
                return Ok(apartmentNames);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving apartment names.");
                return StatusCode(500, "Internal server error while retrieving apartment names.");
            }
        }

        // Get apartment details by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<(string Name, List<byte[]> Images)>> GetApartmentDetailsById(int id)
        {
            _logger.LogInformation("Retrieving apartment details for ID: {ApartmentId}", id);

            try
            {
                var apartmentDetails = await _apartmentService.GetApartmentDetailsById(id);

                if (apartmentDetails.Name == null)
                {
                    return NotFound($"Apartment with ID {id} not found.");
                }

                return Ok(apartmentDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving apartment details for ID: {ApartmentId}", id);
                return StatusCode(500, "Internal server error while retrieving apartment details.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApartmentDto>>> GetAllApartments([FromQuery] getAllApartmentsDto model)
        {
            var apartments = await _apartmentService.GetAllApartments(model);
            return Ok(new { TotalCount = apartments.Count(), Apartments = apartments });
        }

        // Rent the apartment by Muhnnad
        [HttpPost]
        public async Task<IActionResult> RentApartment([FromBody] RentApartmentDto rentApartmentDto)
        {
            if (rentApartmentDto == null)
            {
                return BadRequest("Rental data is required.");
            }

            // Call the service method to rent the apartment
            var result = await _apartmentService.RentApartment(rentApartmentDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message); // Return the error message if rental fails
            }

            // Optionally check if the apartment is fully rented
            if (result.ApartmentId.HasValue)
            {
                // Additional checks or logging can be added here if needed
            }

            return Ok(result.Message); // Return success message
        }
    }
}
