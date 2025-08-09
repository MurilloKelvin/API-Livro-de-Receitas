using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions.ExceptionsBase;
using AutoMapper;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase
{
    public ResponseRegisteredUsersJson Execute(RequestRegisterUserJson request)
    {
        Validate(request);
        
        var autoMapper = new MapperConfiguration(options =>
        {
            options.AddProfile(new MyRecipeBook.Application.AutoMapper.AutoMapping());  // usar o caminho completo do automapping para nao dar conflito com o automapping do ASP.NET Core
        }).CreateMapper();

        var user = autoMapper.Map<Domain.Entities.User>(request);
        
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