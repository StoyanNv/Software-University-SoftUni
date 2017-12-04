namespace Employees.App
{
    using AutoMapper;
    using Employees.App.DTO;
    using Employees.Models.Models;

    class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<EmployeeDTO, Employee>();
            CreateMap<ManagerDTO , Employee >();
        }
    }
}
