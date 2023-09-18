using DotNetTest.Entities;
using DotNetTest.Repositories;

namespace DotNetTest.Services.impl;

public class DrinkService : IDrinkService
{
    private readonly DrinkRepository _drinkRepository;

    public DrinkService(DrinkRepository drinkRepository)
    {
        _drinkRepository = drinkRepository;
    }

    public void Create(Drink drink)
    {
        _drinkRepository.Save(drink);
    }

    public Drink GetById(int id)
    {
        return _drinkRepository.GetById(id);
    }

    public void Update(Drink drink)
    {
        _drinkRepository.Update(drink);
    }

    public void Delete(Drink drink)
    {
        _drinkRepository.Delete(drink);
    }

    public IEnumerable<Drink> GetAll()
    {
        return _drinkRepository.GetAll();
    }
}