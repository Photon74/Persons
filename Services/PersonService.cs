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

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }

        public bool AddItem(PersonDto item)
        {
            try
            {
                _repository.AddItem(new Person
                {
                    Age = item.Age,
                    Company = item.Company,
                    Email = item.Email,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Id = item.Id,
                });
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
                    : new PersonDto
                {
                    Age = person.Age,
                    Company = person.Company,
                    Email = person.Email,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Id = person.Id,
                };
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
                    result.Add(new PersonDto
                    {
                        Age = person.Age,
                        Company = person.Company,
                        Email = person.Email,
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        Id = person.Id,
                    });
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
                    result.Add(new PersonDto
                    {
                        Age = person.Age,
                        Company = person.Company,
                        Email = person.Email,
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        Id = person.Id,
                    });
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
                _repository.UpdateItem(item.Id, new Person
                {
                    //Id = item.Id,
                    Age = item.Age,
                    Company = item.Company,
                    Email = item.Email,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
