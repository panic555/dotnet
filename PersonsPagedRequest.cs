using DotNetTest.Entities;

namespace DotNetTest.Repositories.Requests;

public class PersonsPagedRequest
{
    public IEnumerable<Person> Persons { get; set; }
    public int TotalCount { get; set; }
}