using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase
{
    public ResponseRegisteredUsersJson Execute(RequestRegisterUserJson request)
    {
        // validar request
        
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
            var errorMessages = result.Errors.Select(e => e.ErrorMessage);
            
            throw new Exception();
        }
        
    }
 
    
}