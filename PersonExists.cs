using System.ComponentModel.DataAnnotations;
using DotNetTest.Services;

namespace DotNetTest.Validators;

//not good, handle in different way
public class PersonExists : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var serviceProvider = validationContext.GetService<IServiceProvider>();
        var personService = serviceProvider.GetRequiredService<IPersonService>();
        var id = (int)value;
        
        if (personService.GetById(id) != null)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("Person with the specified ID does not exist."); //Doesn't really work, could be handled in controller?
    }
}