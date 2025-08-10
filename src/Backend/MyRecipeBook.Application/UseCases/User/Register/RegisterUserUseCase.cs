using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions.ExceptionsBase;
using AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Domain.Repositories.User;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _writeOnlyRepository;
    private readonly IUserReadOnlyRepository _readOnlyRepository;
    public async Task<ResponseRegisteredUsersJson> Execute(RequestRegisterUserJson request)
    {
        Validate(request);
        
        var criptografiaDeSenha = new PasswordEncripter();        

        var autoMapper = new MapperConfiguration(options =>
        {
            options.AddProfile(new MyRecipeBook.Application.AutoMapper.AutoMapping());  // usar o caminho completo do automapping para nao dar conflito com o automapping do ASP.NET Core
        }).CreateMapper();

        var user = autoMapper.Map<Domain.Entities.User>(request);
        
        user.Password = criptografiaDeSenha.Encrypt(request.Password); // encripta a senha antes de salvar no banco de dados
        
        await _writeOnlyRepository.Add(user);
                
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