namespace Application.Dtos;

public record LoginDto(string Email, string Password);
public record RegisterDto(string FirstName, string LastName, string Nick, string Role, string Email, string Password, string ConfirmPassword);