using DotNetTest.Entities;
using DotNetTest.Helpers;
using ISession = NHibernate.ISession;

namespace DotNetTest.Repositories;

public class BrandRepository
{
    private readonly ISession _session;

    public BrandRepository()
    {
        _session = NHibernateHelper.OpenSession();
    }

    public void Save(Brand brand)
    {
        _session.Save(brand);
        _session.Flush();
    }

    public Brand GetById(int id)
    {
        return _session.Get<Brand>(id);
    }

    public void Update(Brand brand)
    {
        _session.Update(brand);
        _session.Flush();
    }

    public void Delete(Brand brand)
    {
        _session.Delete(brand);
        _session.Flush();
    }
}