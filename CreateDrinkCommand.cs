using System.ComponentModel.DataAnnotations;
using DotNetTest.Entities;

namespace DotNetTest.Services.Commands;

public class CreateDrinkCommand
{
    [StringLength(100)]
    public string Name { get; set; }
    public DrinkType Type { get; set; }
    public int Brand { get; set; }
    public bool Deleted { get; set; }
}