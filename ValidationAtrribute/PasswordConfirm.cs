using System.ComponentModel.DataAnnotations;

namespace market.ValidationAtrribute;

public class PasswordConfirm : ValidationAttribute
{
    private readonly string _password;

    public PasswordConfirm(string password)
    {
        _password = password;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var passwordConfirm = value as string;
        var password = validationContext.ObjectType.GetProperty(_password).GetValue(validationContext.ObjectInstance) as string;

        if (passwordConfirm != password)
        {
            return new ValidationResult(ErrorMessage ?? "비밀번호와 비밀번호 확인이 일치하지 않습니다.");
        }
        
        return ValidationResult.Success;
    }
}