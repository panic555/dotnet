using AutoMapper;
using DotNetTest.Entities;
using DotNetTest.Services;
using DotNetTest.Services.Commands;

namespace DotNetTest.Mappers;

public class CreateDrinkCommandMapper
{
    private readonly IBrandService _brandService;

    public CreateDrinkCommandMapper(IBrandService brandService)
    {
        _brandService = brandService;
    }

    public Drink Map(CreateDrinkCommand command)
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateDrinkCommand, Drink>();
        });
        
        var mapper = configuration.CreateMapper();
        var drink = mapper.Map<Drink>(command);
        drink.Brand = _brandService.GetById(command.Brand);

        return drink;
    }
}