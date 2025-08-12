using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions.ExceptionsBase;
using AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Domain.Repositories.User;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _writeOnlyRepository;
    private readonly IUserReadOnlyRepository _readOnlyRepository;
    private readonly IMapper _mapper;
    private readonly PasswordEncripter _passwordEncripter;
    
    public RegisterUserUseCase(
        IUserWriteOnlyRepository writeOnlyRepository,
        IUserReadOnlyRepository readOnlyRepository,
        IMapper mapper,
        PasswordEncripter passwordEncripter)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _passwordEncripter = passwordEncripter;
        _mapper = mapper;
    }
    public async Task<ResponseRegisteredUsersJson> Execute(RequestRegisterUserJson request)
    {
        Validate(request);
        
        var user = _mapper.Map<Domain.Entities.User>(request);
        
        user.Password = _passwordEncripter.Encrypt(request.Password); // encripta a senha antes de salvar no banco de dados
        
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