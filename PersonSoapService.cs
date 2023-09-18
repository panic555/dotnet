using System.Xml.Linq;
using DotNetTest.Entities;
using DotNetTest.Mappers;
using DotNetTest.Repositories;

namespace DotNetTest.Services.impl;

public class PersonSoapService : IPersonSoapService
{
    private readonly PersonRepository _personRepository;

    private readonly CreatePersonCommandMapper _commandMapper;

    public PersonSoapService(PersonRepository personRepository, CreatePersonCommandMapper commandMapper)
    {
        _personRepository = personRepository;
        _commandMapper = commandMapper;
    }

    public string Test(string s)
    {
        Console.WriteLine("Test Method Executed!");
        return s;
    }

    public void XmlMethod(XElement xml)
    {
        Console.WriteLine(xml.ToString());
    }

    public SoapPerson TestPerson(SoapPerson person)
    {
        return person;
    }

    public Person Save(SoapPerson person)
    {
        var newPerson = _commandMapper.Map(person);
        _personRepository.Save(newPerson);

        return newPerson;
    }
}