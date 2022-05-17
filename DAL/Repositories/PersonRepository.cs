using Persons.DAL.Entities;
using Persons.DAL.Repositories.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Persons.DAL.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonDbContext _context;

        public PersonRepository(PersonDbContext context)
        {
            _context = context;
        }

        public bool AddItem(Person item)
        {
            try
            {
                _context.Persons.Add(item);
            }
            catch (Exception)
            {
                return false;
            }
            return Commit();
        }

        public bool DeleteItem(int id)
        {
            try
            {
                var person = _context.Persons.SingleOrDefault(p => p.Id == id);
                if (person is not null)
                {
                    person.IsDeleted = true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return Commit();
        }

        public IReadOnlyList<Person> FindItems(string searchQuery)
        {
            try
            {
                return _context.Persons
                    .Where(p => p.FirstName == searchQuery && p.IsDeleted != true)
                    .OrderBy(p => p.Id)
                    .ToList();
            }
            catch (Exception)
            {
                return new List<Person>();
            }
        }

        public Person GetItem(int id)
        {
            try
            {
                return _context.Persons
                    .Where(p => p.IsDeleted == false)
                    .FirstOrDefault(p => p.Id == id);
            }
            catch (Exception)
            {
                return new Person();
            }
        }

        public IReadOnlyList<Person> GetItemsList(int skip, int take)
        {
            try
            {
                return _context.Persons
                    .Where(p => p.IsDeleted == false)
                    .Skip(skip)
                    .Take(take)
                    .OrderBy(p => p.Id)
                    .ToList();
            }
            catch (Exception)
            {
                return new List<Person>();
            }
        }

        public bool UpdateItem(int id, Person item)
        {
            try
            {
                var person = _context.Persons.FirstOrDefault(p => p.Id == id);
                if (person != null)
                {
                    person.FirstName = item.FirstName;
                    person.LastName = item.LastName;
                    person.Email = item.Email;
                    person.Company = item.Company;
                    person.Age = item.Age;
                    person.IsDeleted = item.IsDeleted;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return Commit();
        }

        private bool Commit()
        {
            var count = _context.SaveChanges();
            return count > 0;
        }
    }
}