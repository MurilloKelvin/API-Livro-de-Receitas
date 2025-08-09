using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.API.Controllers;

[Route("[controller]")]
[ApiController]

public class UsersController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUsersJson), (StatusCodes.Status201Created))]

    // responsavel por registrar um usuário
    public IActionResult Register(RequestRegisterUserJson request)
    {
        var useCase = new RegisterUserUseCase();

        var result = useCase.Execute(request);

        return Created(string.Empty, result);
    }
}

