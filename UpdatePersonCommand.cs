using System.ComponentModel.DataAnnotations;
using DotNetTest.Validators;

namespace DotNetTest.Services.Commands;

public class UpdatePersonCommand
{
    [Required]
    //[PersonExists]
    public int Id { get; set; }
    
    [StringLength(100)]
    public string Name { get; set; }
    
    [StringLength(100)]
    public string Surname { get; set; }
    
    //Another unique validator
    //Usually handled in  controller judging by google and chatGPT
    //Constructor issues with this approach + c# and .NET
    public string Username { get; set; }
}