using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Xunit;
using Zotes.Domain.Validation;

namespace Zotes.Tests.Validation;

public class RequiredNonWhiteSpaceAttributeTests
{
    private static ValidationContext Ctx(string memberName)
        => new(new object(), null, null) { MemberName = memberName };

    [Theory]
    [InlineData("Title", "Hello World")]
    [InlineData("Name", "A")]
    public void IsValid_ReturnsSuccess_ForNonEmptyNonWhitespace(string member, string value)
    {
        var attr = new RequiredNonWhiteSpaceAttribute();

        var result = attr.GetValidationResult(value, Ctx(member));

        result.Should().Be(ValidationResult.Success);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void IsValid_Fails_ForNullEmptyOrWhitespace(string? value)
    {
        var attr = new RequiredNonWhiteSpaceAttribute();

        var result = attr.GetValidationResult(value, Ctx("Title"));

        result.Should().NotBeNull();
        result!.ErrorMessage.Should().Be("Title cannot be empty or whitespace.");
        result.MemberNames.Should().Contain("Title");
    }

    [Fact]
    public void IsValid_UsesCustomErrorMessage_WhenProvided()
    {
        var attr = new RequiredNonWhiteSpaceAttribute
        {
            ErrorMessage = "Custom error!"
        };

        var result = attr.GetValidationResult(" ", Ctx("Name"));

        result!.ErrorMessage.Should().Be("Custom error!");
        result.MemberNames.Should().Contain("Name");
    }
}
