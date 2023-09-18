using System.ComponentModel.DataAnnotations;
using DotNetTest.Services.Commands;

namespace DotNetTestTests.Tests.UnitTests;

[TestFixture]
public class CreatePersonCommandTests
{
    [Test]
    public void Name_Should_Have_Maximum_Length_100()
    {
        // Arrange
        var command = new CreatePersonCommand();
        var propertyInfo = command.GetType().GetProperty("Name");
        var stringLengthAttribute = propertyInfo.GetCustomAttributes(typeof(StringLengthAttribute), true)[0] as StringLengthAttribute;

        // Act
        var maxLength = stringLengthAttribute.MaximumLength;

        // Assert
        Assert.That(maxLength, Is.EqualTo(100));
    }   
    
    [Test]
    public void Surname_Should_Have_Maximum_Length_100()
    {
        // Arrange
        var command = new CreatePersonCommand();
        var propertyInfo = command.GetType().GetProperty("Surname");
        var stringLengthAttribute = propertyInfo.GetCustomAttributes(typeof(StringLengthAttribute), true)[0] as StringLengthAttribute;

        // Act
        var maxLength = stringLengthAttribute.MaximumLength;

        // Assert
        Assert.That(maxLength, Is.EqualTo(100));
    }   
}