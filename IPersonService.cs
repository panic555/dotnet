using DotNetTest.Entities;
using DotNetTest.Repositories.Requests;

namespace DotNetTest.Services;

public interface IPersonService
{
    void Create(Person person);
    Person GetById(int id);
    void Update(Person person);
    void Delete(Person person);
    IEnumerable<Person> GetAll();
    PersonsPagedRequest GetAll(int page, int pageSize);
    decimal GetAmountConsumed(Person person);
}