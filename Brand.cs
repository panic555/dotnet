namespace DotNetTest.Entities;

public class Brand
{
    public virtual int Id { get; set; }
    public virtual string Name { get; set; }
    public virtual bool Deleted { get; set; }
}