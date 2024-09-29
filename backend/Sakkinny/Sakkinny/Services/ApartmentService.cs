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

            if (apartmentDto.Images == null || !apartmentDto.Images.Any())
            {
                _logger.LogWarning("Attempted to add an apartment without images: {ApartmentName}", apartmentDto.title);
                throw new ArgumentException("At least one image is required.");
            }

            _logger.LogInformation("Adding apartment: {ApartmentName}", apartmentDto.title);

            try
            {
                // Manually handle the image conversion from IFormFile to byte[]
                apartment.Images = new List<ApartmentImage>();

                foreach (var imageFile in apartmentDto.Images)
                {
                    using var memoryStream = new MemoryStream();
                    await imageFile.CopyToAsync(memoryStream);

                    var apartmentImage = new ApartmentImage
                    {
                        ImageData = memoryStream.ToArray(),  // Convert image to byte array
                        Apartment = apartment                 // Associate image with apartment
                    };

                    apartment.Images.Add(apartmentImage);
                }


                await _context.Apartments.AddAsync(apartment);
                await _context.SaveChangesAsync();


                var apartmentDtoResult = _mapper.Map<ApartmentDto>(apartment);

                _logger.LogInformation("Apartment added with ID: {ApartmentId}", apartmentDtoResult.Id);
                return apartmentDtoResult;
            }
            catch (ArgumentException argEx)
            {

                _logger.LogError(argEx, "Validation error adding apartment: {ApartmentName}", apartmentDto.title);
                throw;  
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

                var apartment = await _context.Apartments
                    .Include(a => a.Images)
                    .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);


                if (apartment == null)
                {
                    _logger.LogWarning("Apartment with ID: {ApartmentId} not found", id);
                    return null; 
                }

                if (apartmentDto == null)
                {
                    throw new ArgumentNullException(nameof(apartmentDto), "UpdateApartmentDto cannot be null");
                }

                _logger.LogInformation("Updating apartment with data: {@ApartmentDto}", apartmentDto);

                if (apartmentDto.Images != null && apartmentDto.Images.Count > 0)
                {
                    apartment.Images.Clear();

                    foreach (var file in apartmentDto.Images)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);

                            var apartmentImage = new ApartmentImage
                            {
                                ImageData = memoryStream.ToArray(), 
                                Apartment = apartment // Associate the image with the apartment
                            };

                            // Add the new image to the apartment's Images collection
                            apartment.Images.Add(apartmentImage);
                        }
                    }
                }

                await _context.SaveChangesAsync();

                var apartmentDtoResult = _mapper.Map<ApartmentDto>(apartment);

                _logger.LogInformation("Apartment updated successfully with ID: {ApartmentId}", apartmentDtoResult.Id);
                return apartmentDtoResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating apartment with ID: {ApartmentId}", id);
                throw new ApplicationException("Error updating apartment", ex);
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

        public async Task<IEnumerable<string>> GetAllApartmentNames()
        {
            _logger.LogInformation("Retrieving all apartment names.");

            try
            {
                var apartmentNames = await _context.Apartments
                    .Where(a => !a.IsDeleted)
                    .Select(a => a.Title)  
                    .ToListAsync();

                _logger.LogInformation("Retrieved {Count} apartment names.", apartmentNames.Count);
                return apartmentNames;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving apartment names.");
                throw new ApplicationException("Error retrieving apartment names", ex);
            }
        }

        public async Task<(string Name, List<byte[]> Images)> GetApartmentDetailsById(int id)
        {
            _logger.LogInformation("Attempting to retrieve apartment details for ID: {ApartmentId}", id);

            try
            {
                var apartment = await _context.Apartments
                    .Include(a => a.Images)
                    .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);

                if (apartment == null)
                {
                    _logger.LogWarning("Apartment with ID: {ApartmentId} not found", id);
                    return (null, null);
                }

                var images = apartment.Images.Select(img => img.ImageData).ToList();

                _logger.LogInformation("Retrieved apartment details for ID: {ApartmentId}", id);
                return (apartment.Title, images);  
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving apartment details for ID: {ApartmentId}", id);
                throw new ApplicationException("Error retrieving apartment details", ex);
            }
        }

        public async Task<IEnumerable<ApartmentDto>> GetAllApartments(getAllApartmentsDto model)
        {
            var query = _context.Apartments.AsQueryable();
            query = query.Where(a => !a.IsDeleted);
            if (!string.IsNullOrWhiteSpace(model.SearchTerm))
            {
                query = query.Where(a => a.Title.Contains(model.SearchTerm) || a.SubTitle.Contains(model.SearchTerm) || a.Location.Contains(model.SearchTerm));
            }
            if (model.ColumnFilters != null && model.ColumnFilters.Any())
            {
                foreach (var filter in model.ColumnFilters)
                {
                    if (!string.IsNullOrWhiteSpace(filter.Key) && !string.IsNullOrWhiteSpace(filter.Value))
                    {
                        switch (filter.Key.ToLower())
                        {
                            case "title":
                                query = query.Where(a => a.Title.Contains(filter.Value));
                                break;
                            case "location":
                                query = query.Where(a => a.Location.Contains(filter.Value));
                                break;
                            case "roomsnumber":
                                if (int.TryParse(filter.Value, out int roomsNumber))
                                {
                                    query = query.Where(a => a.RoomsNumber == roomsNumber);
                                }
                                break;
                            case "roomsavailable":
                                if (int.TryParse(filter.Value, out int roomsAvailable))
                                {
                                    query = query.Where(a => a.RoomsAvailable == roomsAvailable);
                                }
                                break;
                        }
                    }
                }
            }
            var totalItems = await query.CountAsync();
            var apartments = await query
                .Skip((model.PageIndex - 1) * model.PageSize)
                .Take(model.PageSize)
                .ToListAsync();
            return apartments.Select(apartment => new ApartmentDto
            {
                Id = (int)apartment.Id,
                title = apartment.Title,
                subTitle = apartment.SubTitle,
                location = apartment.Location,
                roomsNumber = apartment.RoomsNumber,
                roomsAvailable = apartment.RoomsAvailable,
                price = apartment.Price,
            }).ToList();
        }
        // rent the apartment  by Muhnnad
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

    // Decrease available rooms
    apartment.RoomsAvailable--;

    // Add the user to the doubly linked list
    RenterList renterList = new RenterList();
    renterList.AddToApartment(rentApartmentDto.ApartmentId, rentApartmentDto.CustomerId, rentApartmentDto.RentalEndDate);



    // Save the updated apartment info
    await _context.SaveChangesAsync();

    return new ResultDto { IsSuccess = true, Message = "Apartment rented successfully.", ApartmentId = apartment.Id };
	}

    }

}



