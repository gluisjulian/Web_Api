using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Api.Model;
using Web_Api.Model.Context;

namespace Web_Api.Repository.Implementation
{
    public class IPersonRepositoryImplementation : IPersonRepository
    {
        private MySqlContext _context;

        //Construtor
        public IPersonRepositoryImplementation(MySqlContext context)
        {
            _context = context;
        }

        //GET -> FindAll
        public List<Person> FindAll()
        {
           return _context.Persons.ToList();
        }

        //GET -> FindById
        public Person FindById(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
        }

        //POST-> Create
        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            return person;
        }

        //POST -> UPDATE
        public Person Update(Person person)
        {
            if (!Exists(person.Id)) return new Person();

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));
            if(result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }

            }

            return person;

        }


        //POST-> DELETE
        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));

            if (result != null)
            {
                try
                {
                    _context.Persons.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        public bool Exists(long id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }
    }
}
