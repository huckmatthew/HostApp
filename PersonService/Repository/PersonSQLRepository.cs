using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PersonService.DTO;
using PersonService.Interfaces;

namespace PersonService.Repository
{
    public class PersonSQLRepository : IPersonRepository
    {
        private readonly string _connectionString;
        public PersonSQLRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["AdventureWorks2014"].ConnectionString;
        }
        public PersonDTO Get(int id)
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var person = connection.Query<PersonDTO>(
                    "SELECT P.[BusinessEntityID], P.[PersonType], P.[NameStyle], P.[Title], P.[FirstName], P.[MiddleName], P.[LastName], P.[Suffix], P.[EmailPromotion], P.[AdditionalContactInfo], P.[Demographics], P.[rowguid], P.[ModifiedDate]  FROM [Person].[Person] P where P.[BusinessEntityID] = @BusinessEntityID",
                        new { BusinessEntityID = id}
                    ).FirstOrDefault();
                connection.Close();
                return person;
            }

        }

        public IEnumerable<PersonDTO> Search(string lastName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var person = connection.Query<PersonDTO>(
                    "SELECT P.[BusinessEntityID], P.[PersonType], P.[NameStyle], P.[Title], P.[FirstName], P.[MiddleName], P.[LastName], P.[Suffix], P.[EmailPromotion], P.[AdditionalContactInfo], P.[Demographics], P.[rowguid], P.[ModifiedDate]  FROM [Person].[Person] P where P.[LastName] like @LastName",
                        new { LastName = lastName +'%' }
                    ).ToArray();
                connection.Close();
                return person;
            }
        }
    }
}
