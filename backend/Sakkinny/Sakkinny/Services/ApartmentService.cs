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



