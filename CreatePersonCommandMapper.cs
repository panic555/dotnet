using AutoMapper;
using DotNetTest.Entities;
using DotNetTest.Services.Commands;

namespace DotNetTest.Mappers;

public class CreatePersonCommandMapper
{
    public Person Map(CreatePersonCommand command)
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreatePersonCommand, Person>();
        });
        
        var mapper = configuration.CreateMapper();
        var person = mapper.Map<Person>(command);

        return person;
    }

    public Person Map(SoapPerson person)
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SoapPerson, Person>();
        });
        
        var mapper = configuration.CreateMapper();
        var newPerson = mapper.Map<Person>(person);

        return newPerson;
    }
}