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
				query = query.Where(a => a.title.Contains(model.SearchTerm) || a.subTitle.Contains(model.SearchTerm) || a.location.Contains(model.SearchTerm));
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
								query = query.Where(a => a.title.Contains(filter.Value));
								break;
							case "location":
								query = query.Where(a => a.location.Contains(filter.Value));
								break;
							case "roomsnumber":
								if (int.TryParse(filter.Value, out int roomsNumber))
								{
									query = query.Where(a => a.roomsNumber == roomsNumber);
								}
								break;
							case "roomsavailable":
								if (int.TryParse(filter.Value, out int roomsAvailable))
								{
									query = query.Where(a => a.roomsAvailable == roomsAvailable);
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
				title = apartment.title,
				subTitle = apartment.subTitle,
				location = apartment.location,
				roomsNumber = apartment.roomsNumber,
				roomsAvailable = apartment.roomsAvailable,
				price = apartment.price,
			}).ToList();
		}
	}
}
