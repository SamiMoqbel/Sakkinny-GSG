using AutoMapper;
using Sakkinny.Models;
using Sakkinny.Models.Dtos;

namespace Sakkinny.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Apartment, ApartmentDto>().ReverseMap();

            CreateMap<CreateApartmentDto, Apartment>().ReverseMap();

            CreateMap<UpdateApartmentDto, Apartment>().ReverseMap();

        }
    }
}
