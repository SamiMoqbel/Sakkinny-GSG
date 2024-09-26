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

        public async Task<ApartmentDto> UpdateApartment(int id, UpdateApartmentDto apartmentDto)
        {
            _logger.LogInformation("Attempting to update apartment with ID: {ApartmentId}", id);

            try
            {
                var apartment = await _context.Apartments.FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
                if (apartment == null)
                {
                    _logger.LogWarning("Apartment with ID: {ApartmentId} not found", id);
                    return null;
                }


                _mapper.Map(apartmentDto, apartment);

                await _context.SaveChangesAsync();

                _logger.LogInformation("Apartment updated with ID: {ApartmentId}", id);

                return _mapper.Map<ApartmentDto>(apartment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating apartment with ID: {ApartmentId}", id);
                throw new InvalidOperationException("Error updating apartment", ex);
            }
        }


        public async Task<ApartmentDto> DeleteApartment(int id)
        {
            _logger.LogInformation("Attempting to delete apartment with ID: {ApartmentId}", id);

            try
            {
                var apartment = await _context.Apartments.FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
                if (apartment == null)
                {
                    _logger.LogWarning("Apartment with ID: {ApartmentId} not found", id);
                    return null;
                }

                apartment.IsDeleted = true;
                apartment.DeletionTime = DateTime.Now;

                await _context.SaveChangesAsync();
                _logger.LogInformation("Apartment marked as deleted with ID: {ApartmentId}", id);
                return _mapper.Map<ApartmentDto>(apartment);
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
            // rent the apartment by Muhnnad
        public async Task<ResultDto> RentApartment(RentApartmentDto rentApartmentDto)
        {
            var apartment = await _context.Apartments.FindAsync(rentApartmentDto.ApartmentId);
    
            if (apartment == null)
            {
                return new ResultDto { IsSuccess = false, Message = "Apartment not found." };
            }

            if (apartment.RoomsAvailable <= 0)
            {
                return new ResultDto { IsSuccess = false, Message = "No rooms available for rent." };
            }

            // if the costamer rent number of rooms be -1
            apartment.RoomsAvailable--;

            await _context.SaveChangesAsync();

            return new ResultDto { IsSuccess = true, Message = "Apartment rented successfully." };
        }
        //get appartment how user want it
        public async Task<Apartment> GetApartmentById(int apartmentId)
        {
            return await _context.Apartments.FindAsync(apartmentId);
        }
          //make apartment rented
        public async Task UpdateApartmentEntity(Apartment apartment)
        {
            _context.Apartments.Update(apartment);
            await _context.SaveChangesAsync();
        }
    }
}