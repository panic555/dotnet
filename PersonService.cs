using DotNetTest.Entities;
using DotNetTest.Repositories;
using DotNetTest.Repositories.Requests;

namespace DotNetTest.Services.impl;

public class PersonService : IPersonService
{
    private readonly PersonRepository _personRepository;

    public PersonService(PersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public void Create(Person person)
    {
        _personRepository.Save(person);
    }

    public Person GetById(int id)
    {
        return _personRepository.GetById(id);
    }

    public void Update(Person person)
    {
        _personRepository.Update(person);
    }

    public void Delete(Person person)
    {
        _personRepository.Delete(person);
    }

    public IEnumerable<Person> GetAll()
    {
        return _personRepository.GetAll();
    }

    public PersonsPagedRequest GetAll(int page, int pageSize)
    {
        return _personRepository.GetAll(page, pageSize);
    }

    public decimal GetAmountConsumed(Person person)
    {
        return _personRepository.GetAmountConsumed(person);
    }
}