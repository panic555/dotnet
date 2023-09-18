using System.ComponentModel.DataAnnotations;
using DotNetTest.Entities;

namespace DotNetTest.Services.Commands;

public class CreateAmountConsumedCommand
{
    public Person Person { get; set; }
    public Drink Drink { get; set; }
    public DateTime TimeOfConsumation { get; set; }
    [Range(0.1, float.MaxValue)]
    public float Amount { get; set; }
}