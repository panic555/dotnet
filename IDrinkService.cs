using DotNetTest.Entities;

namespace DotNetTest.Services;

public interface IDrinkService
{
    void Create(Drink drink);
    Drink GetById(int id);
    void Update(Drink drink);
    void Delete(Drink drink);
    IEnumerable<Drink> GetAll();
}