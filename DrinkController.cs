using DotNetTest.Entities;
using DotNetTest.Mappers;
using DotNetTest.Services;
using DotNetTest.Services.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DotNetTest.Controllers;

[ApiController]
[Route("drinks")]
public class DrinkController : ControllerBase
{
    private readonly IDrinkService _drinkService;
    private readonly CreateDrinkCommandMapper _createDrinkCommandMapper;

    public DrinkController(IDrinkService drinkService, CreateDrinkCommandMapper createDrinkCommandMapper)
    {
        _drinkService = drinkService;
        _createDrinkCommandMapper = createDrinkCommandMapper;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var drink = _drinkService.GetById(id);

        if (drink == null)
        {
            return NotFound();
        }

        return Ok(drink);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var drinks = _drinkService.GetAll();

        if (drinks.IsNullOrEmpty())
        {
            return NotFound();
        }

        return Ok(drinks);
    }

    [HttpGet("/create")]
    public IActionResult Create(CreateDrinkCommand command)
    {
        var drink = _createDrinkCommandMapper.Map(command);
        _drinkService.Create(drink);

        return CreatedAtAction(nameof(GetById), new { id = drink.Id }, drink);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(Drink drink)
    {
        if (drink == null)
        {
            return NotFound();
        }

        _drinkService.Update(drink);

        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var drink = _drinkService.GetById(id);
        
        if (drink == null)
        {
            return NotFound();
        }

        _drinkService.Delete(drink);

        return NoContent();
    }
}