using AutoMapper;

using Persons.Controllers.DTO;
using Persons.DAL.Entities;
using Persons.DAL.Repositories.Interfaces;
using Persons.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Persons.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public bool AddItem(PersonDto item)
        {
            try
            {
                _repository.AddItem(_mapper.Map<Person>(item));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteItem(int id)
        {
            try
            {
                _repository.DeleteItem(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public PersonDto GetById(int id)
        {
            try
            {
                var person = _repository.GetItem(id);
                return person == null
                    ? null
                    : _mapper.Map<PersonDto>(person);
            }
            catch (Exception)
            {
                return new PersonDto();
            }
        }

        public IReadOnlyList<PersonDto> GetByName(string name)
        {
            try
            {
                var persons = _repository.FindItems(name);
                return persons.Select(person => _mapper.Map<PersonDto>(person)).ToList();
            }
            catch (Exception)
            {
                return new List<PersonDto>();
            }
        }

        public IReadOnlyList<PersonDto> GetItemsList(int skip, int take)
        {
            try
            {
                var persons = _repository.GetItemsList(skip, take);
                return persons.Select(person => _mapper.Map<PersonDto>(person)).ToList();
            }
            catch (Exception)
            {
                return new List<PersonDto>();
            }
        }

        public bool UpdateItem(PersonDto item)
        {
            try
            {
                _repository.UpdateItem(item.Id, _mapper.Map<Person>(item));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
