using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase
{
    public ResponseRegisteredUsersJson Execute(RequestRegisterUserJson request)
    {
        Validate(request);
        
        var user = new Domain.Entities.User
        {
            Name = request.Name,
            Email = request.Email
        };
        
        // map request to entity]
        
        // criptografar senha
        
        // salvar entity no banco de dados
        
        return new ResponseRegisteredUsersJson
        {
            Name = request.Name,
        };
    }

    private void Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            
            throw new ErrorOnValidationException(errorMessages);
        }
        
    }
 
    
}