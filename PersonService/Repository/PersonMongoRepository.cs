using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HostApp.Core.Interfaces;
using HostApp.Core.Repository;
using MongoDB.Driver;
using PersonService.DTO;
using PersonService.Interfaces;

namespace PersonService.Repository
{
    public class PersonMongoRepository : RespositoryMongoDBBase<PersonDTO>, IPersonRepository
    {
        public PersonMongoRepository(IContextMongoDB<PersonDTO> context) : base(context)
        {
            
        }

        public PersonDTO Get(int id)
        {
            var data = Context.GetCollection.Find(d=>d.businessEntityId.Equals(id));

            return data.FirstOrDefault();

        }

        public IEnumerable<PersonDTO> Search(string lastName)
        {
            Expression<Func<PersonDTO, bool>> filter = x => x.lastName.StartsWith(lastName);
            var data = Context.GetCollection.Find(filter).ToList();
            return data;
        }
    }
}
