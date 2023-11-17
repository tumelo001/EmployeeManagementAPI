using AutoMapper;
using Employee_Management.Data;
using Employee_Management.Models;

namespace Employee_Management.Models.Dtos
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Employee, EmployeeGetDto>()
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position.Name))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name));
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();
            CreateMap<Position, PositionGetDto>();
            CreateMap<PositionCreateDto, Position>();
        }
    }
}
