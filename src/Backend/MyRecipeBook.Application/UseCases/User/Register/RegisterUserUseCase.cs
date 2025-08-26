using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions.ExceptionsBase;
using AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAcces.Repositories;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _writeOnlyRepository;
    private readonly IUserReadOnlyRepository _readOnlyRepository;
    private readonly IUnityOfWork _unityOfWork;
    private readonly IMapper _mapper;
    private readonly PasswordEncripter _passwordEncripter;
    
    public RegisterUserUseCase(
        IUserWriteOnlyRepository writeOnlyRepository,
        IUserReadOnlyRepository readOnlyRepository,
        IUnityOfWork unityOfWork,
        IMapper mapper,
        PasswordEncripter passwordEncripter)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _unityOfWork = unityOfWork;
        _passwordEncripter = passwordEncripter;
        _mapper = mapper;
    }
    public async Task<ResponseRegisteredUsersJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);
        
        var user = _mapper.Map<Domain.Entities.User>(request);
        
        user.Password = _passwordEncripter.Encrypt(request.Password); // encripta a senha antes de salvar no banco de dados
        
        await _writeOnlyRepository.Add(user);
        
        await _unityOfWork.Commit(); // salva as alterações no banco de dados
                
        return new ResponseRegisteredUsersJson
        {
            Name = request.Name,
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

      var emailExist = await _readOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if(emailExist)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "Já existe um usuário ativo com este e-mail."));
        }

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            
            throw new ErrorOnValidationException(errorMessages);
        }
        
    }
 
    
}