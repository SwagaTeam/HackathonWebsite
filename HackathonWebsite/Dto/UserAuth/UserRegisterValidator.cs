using FluentValidation;

namespace HackathonWebsite.DTO;

public class UserRegisterValidator : AbstractValidator<UserAuthDto>
{
    private const string specialChars = "!@#$%^&*";
    public UserRegisterValidator()
    {
        RuleFor(u=>u.FullName)
            .NotEmpty().WithMessage("Full name is required")
            .Length(2,50).WithMessage("Full name must be between 3 and 50 characters");
        
        RuleFor(u=>u.Telegram)
            .NotEmpty().WithMessage("Telegram is required")
            .Length(2,50).WithMessage("Telegram must be between 3 and 50 characters");
        
        //Не понял про тим айди оставлю пока заглушку
        RuleFor(u=>u.TeamId)
            .NotEmpty().WithMessage("Team id is required");
            
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email обязателен для заполнения")
            .EmailAddress().WithMessage("Неверный формат email");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Пароль обязателен для заполнения")
            .Length(8, 70).WithMessage("Пароль должен содержать от 8 до 70 символов")
            .Must(ContainUpperAndLower).WithMessage("Пароль должен содержать хотя бы одну заглавную и одну строчную букву")
            .Must(ContainSpecialCharacter).WithMessage("Пароль должен содержать хотя бы один специальный символ: !@#$%^&*");
    }

    private bool ContainUpperAndLower(string password)
    {
        return password.Any(char.IsUpper) && password.Any(char.IsLower);
    }

    private bool ContainSpecialCharacter(string password)
    {
        return password.Any(c => specialChars.Contains(c));
    }
}