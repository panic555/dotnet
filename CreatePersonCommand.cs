using System.ComponentModel.DataAnnotations;
using DotNetTest.Validators;

namespace DotNetTest.Services.Commands;

public class CreatePersonCommand
{
    [StringLength(100)]
    public string Name { get; set; }
    
    [StringLength(100)]
    public string Surname { get; set; }
    
    public string Username { get; set; }
}