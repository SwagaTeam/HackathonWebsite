using FluentValidation;

namespace HackathonWebsite.DTO;

public class UserRegisterValidator : AbstractValidator<UserAuthDto>
{
    private const string specialChars = "!@#$%^&*"; // Список спецсимволов
    public UserRegisterValidator()
    {
        RuleFor(u => u.FullName)
            .NotEmpty().WithMessage("Полное имя обязательно для заполнения")
            .Length(2, 50).WithMessage("Полное имя должно быть от 3 до 50 символов"); 
        
        RuleFor(u => u.Telegram)
            .NotEmpty().WithMessage("Telegram обязателен для заполнения") 
            .Length(2, 50).WithMessage("Telegram должен быть от 3 до 50 символов"); 

        //добавить проверку на существование комманды если вводят
        RuleFor(u => u.TeamId)
            .GreaterThanOrEqualTo(0).WithMessage("ID команды должен быть больше или равен 0") // Проверка на минимальное значение
            .When(u => u.TeamId.HasValue); // Применяется только если TeamId задан

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email обязателен для заполнения")
            .EmailAddress().WithMessage("Неверный формат email"); 

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Пароль обязателен для заполнения") 
            .Length(8, 70).WithMessage("Пароль должен содержать от 8 до 70 символов") // Проверка длины пароля
            .Must(ContainUpperAndLower).WithMessage("Пароль должен содержать хотя бы одну заглавную и одну строчную букву") // Проверка на заглавные и строчные буквы
            .Must(ContainSpecialCharacter).WithMessage("Пароль должен содержать хотя бы один специальный символ: !@#$%^&*"); // Проверка на спецсимволы
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
