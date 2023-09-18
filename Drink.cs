namespace DotNetTest.Entities;

public class Drink
{
    public virtual int Id { get; set; }
    public virtual string Name { get; set; }
    public virtual DrinkType Type { get; set; }
    public virtual Brand Brand { get; set; }
    public virtual bool Deleted { get; set; }
}