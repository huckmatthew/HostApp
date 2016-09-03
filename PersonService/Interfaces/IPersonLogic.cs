using System.Collections.Generic;
using PersonService.DTO;

namespace PersonService.Interfaces
{
    public interface IPersonLogic
    {
        PersonDTO Get(int id);

        IEnumerable<PersonDTO> Search(string lastName);
    }
}
