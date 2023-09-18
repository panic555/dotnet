namespace DotNetTest.Entities;

public class AmountConsumed
{
    public virtual int Id { get; set; }
    public virtual Person Person { get; set; }
    public virtual Drink Drink { get; set; }
    public virtual DateTime TimeOfConsumation { get; set; }
    public virtual decimal Amount { get; set; }
}