using AutoMapper;
using Sakkinny.Models;
using Sakkinny.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Sakkinny.Services
{
    public class ApartmentService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly ILogger<ApartmentService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApartmentService(IMapper mapper, DataContext context, ILogger<ApartmentService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
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
            if (apartmentDto.roomsNumber <= apartmentDto.roomsAvailable)
            {
                _logger.LogWarning("Validation error for apartment: {ApartmentName}. RoomsNumber must be greater than RoomsAvailable.", apartmentDto.title);
                throw new ArgumentException("RoomsNumber must be greater than RoomsAvailable.");
            }

            if (apartmentDto.price <= 0)
            {
                _logger.LogWarning("Validation error for apartment: {ApartmentName}. Price must be greater than zero.", apartmentDto.title);
                throw new ArgumentException("Price must be greater than zero.");
            }

            _logger.LogInformation("Adding apartment: {ApartmentName}", apartmentDto.title);

            try
            {

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

        public async Task<getApartmentDetailsDto> GetApartmentDetailsById(int id)
        {
            _logger.LogInformation("Attempting to retrieve apartment data with ID: {ApartmentId}", id);

            try
            {
                var apartment = await _context.Apartments
                    .Include(a => a.Images)
                    .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);

                if (apartment == null)
                {
                    return null;
                }

                var base64Images = apartment.Images
                    .Select(img => Convert.ToBase64String(img.ImageData))
                    .ToList();

                return new getApartmentDetailsDto
                {
                    Title = apartment.Title,
                    Base64Images = base64Images,
                    subTitle = apartment.SubTitle,
                    location = apartment.Location,
                    roomsNumber = apartment.RoomsNumber,
                    roomsAvailable = apartment.RoomsAvailable,
                    price = apartment.Price

                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving apartment with ID: {ApartmentId}", id);
                throw new InvalidOperationException("Error retrieving apartment", ex);
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
                    if (!string.IsNullOrWhiteSpace(filter.Key) && filter.Value != null && filter.Value.Any())
                    {
                        switch (filter.Key.ToLower())
                        {
                            case "title":
                                query = query.Where(a => filter.Value.Any(val => a.Title.Contains(val)));
                                break;
                            case "location":
                                query = query.Where(a => filter.Value.Any(val => a.Location.Contains(val)));
                                break;
                            case "roomsnumber":
                                var roomsNumbers = filter.Value
                                    .Select(val => int.TryParse(val, out var number) ? number : (int?)null)
                                    .Where(val => val.HasValue)
                                    .ToList();
                                if (roomsNumbers.Any())
                                {
                                    query = query.Where(a => roomsNumbers.Contains(a.RoomsNumber));
                                }
                                break;
                            case "roomsavailable":
                                var roomsAvailables = filter.Value
                                    .Select(val => int.TryParse(val, out var available) ? available : (int?)null)
                                    .Where(val => val.HasValue)
                                    .ToList();
                                if (roomsAvailables.Any())
                                {
                                    query = query.Where(a => roomsAvailables.Contains(a.RoomsAvailable));
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
       // Rent the apartment
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

        // Get Apartments by OwnerId
        public async Task<IEnumerable<ApartmentDto>> GetApartmentByOwnerId(string ownerId)
        {
            var apartments = await _context.Apartments
                .Where(a => a.OwnerId == ownerId && !a.IsDeleted)
                .ToListAsync();

            return apartments.Select(a => new ApartmentDto
            {
                Id = a.Id,
                title = a.Title,
                subTitle = a.SubTitle,
                location = a.Location,
                roomsNumber = a.RoomsNumber,
                roomsAvailable = a.RoomsAvailable,
                price = a.Price,
            });
        }

        // Get Customers by OwnerId and ApartmentId
        public async Task<IEnumerable<CustomerDto>> GetCustomersByOwnerAndApartmentId(string ownerId, int apartmentId)
        {
            var apartment = await _context.Apartments
                .FirstOrDefaultAsync(a => a.OwnerId == ownerId && a.Id == apartmentId && !a.IsDeleted);

            if (apartment == null)
            {
                return Enumerable.Empty<CustomerDto>();
            }

            var renterList = new RenterList(); // Assuming the apartment keeps track of the renters
            var customers = renterList.GetAllRenters(); // This should return a list of renters

            return customers.Select(c => new CustomerDto
            {
                CustomerId = c.CustomerId,
            });
        }
    }
}