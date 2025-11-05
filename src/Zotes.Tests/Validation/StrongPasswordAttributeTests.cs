using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Xunit;
using Zotes.Domain.Validation;

namespace Zotes.Tests.Validation;

public class StrongPasswordAttributeTests
{
    private static ValidationContext Ctx(string memberName = "Password")
        => new(new object(), null, null) { MemberName = memberName };

    [Theory]
    [InlineData("Abcdefg1")] // exactly 8, valid mix
    [InlineData("P@ssw0rdWithUpperAndLower123")] // longer, valid mix
    public void IsValid_ReturnsSuccess_ForStrongPasswords(string password)
    {
        var attr = new StrongPasswordAttribute();

        var result = attr.GetValidationResult(password, Ctx());

        result.Should().Be(ValidationResult.Success);
    }

    [Theory]
    [InlineData(null, "Password is required.")]
    public void IsValid_Fails_WhenNull(object? password, string expectedMessage)
    {
        var attr = new StrongPasswordAttribute();

        var result = attr.GetValidationResult(password, Ctx());

        result!.ErrorMessage.Should().Be(expectedMessage);
    }

    [Fact]
    public void IsValid_Fails_WhenTooShort()
    {
        var attr = new StrongPasswordAttribute();

        var result = attr.GetValidationResult("Abc1", Ctx());

        result!.ErrorMessage.Should().Be("Password must be at least 8 characters long.");
    }

    [Fact]
    public void IsValid_Fails_WhenMissingUppercase()
    {
        var attr = new StrongPasswordAttribute();

        var result = attr.GetValidationResult("abcdefg1", Ctx());

        result!.ErrorMessage.Should().Be("Password must contain at least one uppercase letter.");
    }

    [Fact]
    public void IsValid_Fails_WhenMissingLowercase()
    {
        var attr = new StrongPasswordAttribute();

        var result = attr.GetValidationResult("ABCDEFG1", Ctx());

        result!.ErrorMessage.Should().Be("Password must contain at least one lowercase letter.");
    }

    [Fact]
    public void IsValid_Fails_WhenMissingDigit()
    {
        var attr = new StrongPasswordAttribute();

        var result = attr.GetValidationResult("Abcdefgh", Ctx());

        result!.ErrorMessage.Should().Be("Password must contain at least one digit.");
    }
}
