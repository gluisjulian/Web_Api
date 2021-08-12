using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Api.Model;
using Web_Api.Model.Context;
using Web_Api.Repository;

namespace Web_Api.Business.Implementation
{
    public class IPersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;

        //Construtor
        public IPersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
        }

        //GET -> FindAll
        public List<Person> FindAll()
        {
           return _repository.FindAll();
        }

        //GET -> FindById
        public Person FindById(long id)
        {
            return _repository.FindById(id);
        }

        //POST-> Create
        public Person Create(Person person)
        {
            return _repository.Create(person);
        }

        //POST -> UPDATE
        public Person Update(Person person)
        {
            return _repository.Update(person);
        }


        //POST-> DELETE
        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
