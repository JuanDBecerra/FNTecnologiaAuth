using AutoMapper;
using Prueba.Domain.Entities.Dtos;
using Prueba.Domain.Entities.Model;

namespace Prueba.Infraestructure.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Users, UserDto>()
                .ForMember(d => d.FirstName, opt => opt.MapFrom(src => src.Usr_FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(src => src.Usr_LastName))
                .ForMember(d => d.DocumentNumber, opt => opt.MapFrom(src => src.Usr_DocumentNumber))
                .ForMember(d => d.Rol, opt => opt.MapFrom(src => src.Rols.Rol_Description))
                .ReverseMap();
        }
    }
}
