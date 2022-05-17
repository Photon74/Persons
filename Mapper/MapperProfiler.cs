using AutoMapper;

using Persons.Controllers.DTO;
using Persons.DAL.Entities;

namespace Persons.Mapper
{
    public class MapperProfiler : Profile
    {
        public MapperProfiler()
        {
            CreateMap<Person, PersonDto>();
            CreateMap<PersonDto, Person>();
        }
    }
}
