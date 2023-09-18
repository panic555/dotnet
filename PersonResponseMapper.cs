using AutoMapper;
using DotNetTest.Controllers.Responses;
using DotNetTest.Entities;
using DotNetTest.Services;

namespace DotNetTest.Mappers;

public class PersonResponseMapper
{
    private readonly IPersonService _personService;

    public PersonResponseMapper(IPersonService personService)
    {
        _personService = personService;
    }

    public PersonResponse Map(Person person)
    {
        if (person == null)
        {
            return null; //exception
        }
        var amountConsumed = _personService.GetAmountConsumed(person);
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Person, PersonResponse>();
        });
        
        var mapper = configuration.CreateMapper();
        var personResponse = mapper.Map<PersonResponse>(person);
        personResponse.AmountConsumed = amountConsumed;

        return personResponse;
    }
}