using DotNetTest.Entities;
using DotNetTest.Helpers;
using ISession = NHibernate.ISession;

namespace DotNetTest.Repositories;

public class DrinkRepository
{
    private readonly ISession _session;

    public DrinkRepository()
    {
        _session = NHibernateHelper.OpenSession();
    }

    public void Save(Drink drink)
    {
        _session.Save(drink);
        _session.Flush();
    }

    public Drink GetById(int id)
    {
        return _session.Get<Drink>(id);
    }

    public void Update(Drink drink)
    {
        _session.Update(drink);
        _session.Flush();
    }

    public void Delete(Drink drink)
    {
        _session.Delete(drink);
        _session.Flush();
    }
    
    public IEnumerable<Drink> GetAll()
    {
        return _session.Query<Drink>().Where(d => d.Deleted != true).ToList();
    }
}