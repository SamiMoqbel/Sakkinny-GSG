using AutoMapper;
using Sakkinny.Models;
using Sakkinny.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Sakkinny.Services
{
    public class ApartmentService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly ILogger<ApartmentService> _logger;

        public ApartmentService(IMapper mapper, DataContext context, ILogger<ApartmentService> logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public async Task<ApartmentDto> AddApartment(CreateApartmentDto apartmentDto)
        {
            var apartment = _mapper.Map<Apartment>(apartmentDto);
            apartment.CreationTime = DateTime.Now; 
            _logger.LogInformation("ADDING apartment: {ApartmentName}", apartmentDto.title);

            try
            {
                await _context.Apartments.AddAsync(apartment);
                await _context.SaveChangesAsync();

                var mapping = _mapper.Map<ApartmentDto>(apartment);
                _logger.LogInformation("Apartment added with ID: {ApartmentId}", mapping.Id);

                return mapping;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding apartment: {ApartmentName}", apartmentDto.title);
                throw new ApplicationException("Error adding apartment", ex);
            }
        }

        public async Task<bool> UpdateApartment(int id, UpdateApartmentDto apartmentDto)
        {
            _logger.LogInformation("Attempting to update apartment with ID: {ApartmentId}", id);

            try
            {
                var apartment = await _context.Apartments.FirstOrDefaultAsync(a => a.Id == id);
                if (apartment == null)
                {
                    _logger.LogWarning("Apartment with ID: {ApartmentId} not found", id);
                    return false;
                }

                _mapper.Map(apartmentDto, apartment);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Apartment updated with ID: {ApartmentId}", id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating apartment with ID: {ApartmentId}", id);
                throw new InvalidOperationException("Error updating apartment", ex);
            }
        }

        public async Task<bool> DeleteApartment(int id)
        {
            _logger.LogInformation("Attempting to delete apartment with ID: {ApartmentId}", id);

            try
            {
                var apartment = await _context.Apartments.FirstOrDefaultAsync(a => a.Id == id);
                if (apartment == null)
                {
                    _logger.LogWarning("Apartment with ID: {ApartmentId} not found", id);
                    return false;
                }

                apartment.IsDeleted = true; 
                apartment.DeletionTime = DateTime.Now;

                await _context.SaveChangesAsync();
                _logger.LogInformation("Apartment marked as deleted with ID: {ApartmentId}", id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking apartment as deleted with ID: {ApartmentId}", id);
                throw new InvalidOperationException("Error deleting apartment", ex);
            }
        }

        public async Task<IEnumerable<ApartmentDto>> GetAllApartments()
        {
            var apartmentsDtos = new List<ApartmentDto>();
            var retrievedApartments = await _context.Apartments.ToListAsync();

            foreach (var apartment in retrievedApartments)
            {
                if (!apartment.IsDeleted) 
                {
                    var apartmentDto = _mapper.Map<ApartmentDto>(apartment);
                    apartmentsDtos.Add(apartmentDto);
                }
            }
            return apartmentsDtos;
        }
    }
}
