using DotNetTest.Entities;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Dialect;
using ISession = NHibernate.ISession;

namespace DotNetTest.Helpers
{
    public static class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        public static ISession OpenSession()
        {
            if (_sessionFactory == null)
            {
                var configuration = new Configuration();
                configuration.DataBaseIntegration(db =>
                {
                    db.Dialect<MySQLDialect>(); // Specify the MySQL dialect
                    db.ConnectionString = "Server=localhost;Database=dotnet;Uid=root;Pwd=mushinz69"; // My data to connect to local database. Should probably be parameterized
                });

                //Map each table
                var modelMapper = new ModelMapper();
                modelMapper.AddMapping<PersonMap>();
                modelMapper.AddMapping<BrandMap>();
                modelMapper.AddMapping<DrinkMap>();
                modelMapper.AddMapping<AmountConsumedMap>();
                var mapping = modelMapper.CompileMappingForAllExplicitlyAddedEntities();
                configuration.AddMapping(mapping);

                _sessionFactory = configuration.BuildSessionFactory();
            }

            return _sessionFactory.OpenSession();
        }
        //Single table mappings
        private class PersonMap : ClassMapping<Person>
        {
            public PersonMap()
            {
                Table("person");
                Id(p => p.Id, m => m.Generator(Generators.Identity));
                Property(p => p.Name);
                Property(p => p.Surname);
                Property(p => p.Username);
            }
        }

        private class BrandMap : ClassMapping<Brand>
        {
            public BrandMap()
            {
                Table("brand");
                Id(b => b.Id, m => m.Generator(Generators.Identity));
                Property(b => b.Name);
                Property(b => b.Deleted);
            }
        }

        private class DrinkMap : ClassMapping<Drink>
        {
            public DrinkMap()
            {
                Table("drink");
                Id(d => d.Id, m => m.Generator(Generators.Identity));
                Property(d => d.Name);
                Property(d => d.Type);
                Property(d => d.Deleted);
                ManyToOne(d => d.Brand, m =>
                {
                    m.Column("brand");
                    m.Cascade(Cascade.None);
                });
            }
        }

        private class AmountConsumedMap : ClassMapping<AmountConsumed>
        {
            public AmountConsumedMap()
            {
                Table("amountconsumed");
                Id(a => a.Id, m => m.Generator(Generators.Identity));
                ManyToOne(a => a.Person, m =>
                {
                    m.Column("person");
                    m.Cascade(Cascade.None);
                });
                ManyToOne(a => a.Drink, m =>
                {
                    m.Column("drink");
                    m.Cascade(Cascade.None);
                });
                Property(a => a.TimeOfConsumation);
                Property(a => a.Amount);
            }
        }
    }
}
