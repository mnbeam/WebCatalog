using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using WebCatalog.Domain.Entities;
using WebCatalog.Logic.Configurations;
using WebCatalog.Logic.Services.Accounts.Dtos;

namespace WebCatalog.Logic.Validators;

internal class AuthValidator
{
    private readonly AuthOptions _authOptions;
    private const string EmailRegex =
        @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]{2,}\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

    public AuthValidator(IOptions<AuthOptions> authOptions)
    {
        _authOptions = authOptions.Value;
    }

    public bool IsValidRegisterData(RegisterDto registerDto)
    {
        return !string.IsNullOrWhiteSpace(registerDto.UserName) &&
               registerDto.UserName.Length < _authOptions.UserNameMaxLength &&
               registerDto.UserName.Length > _authOptions.UserNameMinLength &&
               !string.IsNullOrWhiteSpace(registerDto.Email) &&
               registerDto.Email.Length < _authOptions.UserNameMaxLength &&
               Regex.IsMatch(registerDto.Email, EmailRegex);
    }

    public bool IsValidUserName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) &&
               name.Length >= _authOptions.UserNameMinLength &&
               name.Length <= _authOptions.UserNameMaxLength &&
               Regex.IsMatch(name, "^([a-zA-Z]|[0-9])+$");
    }

    public bool IsValidUser(AppUser user)
    {
        return !string.IsNullOrWhiteSpace(user.UserName) &&
               user.UserName.Length >= _authOptions.UserNameMinLength &&
               user.UserName.Length <= _authOptions.UserNameMaxLength &&
               Regex.IsMatch(user.UserName, "^([a-zA-Z]|[0-9])+$") &&
               user.Id >= 1 &&
               !string.IsNullOrWhiteSpace(user.Email) &&
               Regex.IsMatch(user.Email, EmailRegex);
    }

    public bool IsValidUserId(int userId)
    {
        return userId >= 1;
    }
}