using System.ComponentModel.DataAnnotations;
namespace SharedSilicon.Helpers;

public class CheckboxRequired : ValidationAttribute
{
    public override bool IsValid(object? value) => value is bool b && b;
}


