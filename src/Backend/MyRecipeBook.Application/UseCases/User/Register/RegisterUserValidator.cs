using System.ComponentModel.DataAnnotations;
using FluentValidation;
using FluentValidation.Validators;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    // REGRAS DE VALIDAÇÃO DO USUARIO
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessageException.NAME_EMPTY);
        RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessageException.EMAIL_EMPTY);
        RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessageException.NAME_EMPTY);
        RuleFor(user => user.Password).GreaterThanOrEqualTo(6.ToString()).WithMessage(ResourceMessageException.PASSWORD);
    }
}