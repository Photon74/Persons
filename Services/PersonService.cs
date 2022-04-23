using AutoMapper;

using Persons.Controllers.DTO;
using Persons.DAL.Entities;
using Persons.DAL.Repositories.Intrefaces;

using System;
using System.Collections.Generic;

namespace Persons.Services.Interfaces
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
                List<PersonDto> result = new();
                foreach (var person in persons)
                {
                    result.Add(_mapper.Map<PersonDto>(person));
                }
                return result;
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
                List<PersonDto> result = new();
                foreach (var person in persons)
                {
                    result.Add(_mapper.Map<PersonDto>(person));
                }
                return result;
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
