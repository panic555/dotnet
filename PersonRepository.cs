using DotNetTest.Entities;
using DotNetTest.Helpers;
using DotNetTest.Repositories.Requests;
using ISession = NHibernate.ISession;

namespace DotNetTest.Repositories;

public class PersonRepository
{
    private readonly ISession _session;

    public PersonRepository()
    {
        _session = NHibernateHelper.OpenSession();
    }

    public void Save(Person person)
    {
        _session.Save(person);
        _session.Flush();
    }

    public Person GetById(int id)
    {
        return _session.Get<Person>(id);
    }

    public void Update(Person person)
    {
        _session.Update(person);
        _session.Flush();
    }

    public void Delete(Person person)
    {
        _session.Delete(person);
        _session.Flush();
    }
    
    public IEnumerable<Person> GetAll()
    {
        return _session.Query<Person>().ToList();
    }
    
    public PersonsPagedRequest GetAll(int page, int pageSize)
    {
        var query = _session.Query<Person>();

        var totalCount = query.Count();

        var persons = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PersonsPagedRequest()
        {
            Persons = persons,
            TotalCount = totalCount
        };
    }
    
    public decimal GetAmountConsumed(Person person)
    {
        //for last 7 days
        // var startDate = DateTime.Now.AddDays(-7);
        // var totalAmount = _session.Query<AmountConsumed>()
        //     .Where(ac => ac.Person == person && ac.TimeOfConsumation >= startDate)
        //     .ToList()
        //     .Sum(ac => ac.Amount);
        //
        // return totalAmount;
        var totalAmount = _session.Query<AmountConsumed>()
             .Where(ac => ac.Person == person)
             .ToList()
             .Sum(ac => ac.Amount);

        return totalAmount;
    }
}
