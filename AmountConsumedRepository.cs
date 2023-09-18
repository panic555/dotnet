using DotNetTest.Entities;
using DotNetTest.Helpers;
using ISession = NHibernate.ISession;

namespace DotNetTest.Repositories;

public class AmountConsumedRepository
{
    private readonly ISession _session;

    public AmountConsumedRepository()
    {
        _session = NHibernateHelper.OpenSession();
    }

    public void Save(AmountConsumed amountConsumed)
    {
        _session.Save(amountConsumed);
        _session.Flush();
    }

    public AmountConsumed GetById(int id)
    {
        return _session.Get<AmountConsumed>(id);
    }

    public void Update(AmountConsumed amountConsumed)
    {
        _session.Update(amountConsumed);
        _session.Flush();
    }

    public void Delete(AmountConsumed amountConsumed)
    {
        _session.Delete(amountConsumed);
        _session.Flush();
    }
}