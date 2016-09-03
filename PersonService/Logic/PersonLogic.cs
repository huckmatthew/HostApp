using System.Collections.Generic;
using PersonService.DTO;
using PersonService.Interfaces;
using PersonService.Repository;

namespace PersonService.Logic
{
    public class PersonLogic :IPersonLogic
    {
        private readonly IPersonRepository _repository;

        public PersonLogic(IPersonRepository repository)
        {
            _repository = repository;
        }

        public PersonDTO Get(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<PersonDTO> Search(string lastName)
        {
            return _repository.Search(lastName);
        }
    }
}
