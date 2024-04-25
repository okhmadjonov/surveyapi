using Microsoft.AspNetCore.Mvc;
using surveyapi.Dtos.User;
using surveyapi.Extentions;
using surveyapi.FluentValidation;
using surveyapi.Models;
using surveyapi.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace surveyapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository _authRepository;

    public AuthController(IAuthRepository authRepository) => _authRepository = authRepository;

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var validator = new LoginDtoValidator();
        var validationResult = validator.Validate(loginDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
        }
       
        return ResponseHandler.ReturnIActionResponse(await _authRepository.Login(loginDto));

    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(ResponseModel<UserModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "You can register new user here", Description = "user registration")]
    public async ValueTask<IActionResult> CreateAsync(UserForCreationDto user)
    {
        var validator = new RegisterValidator();
        var validationResult = validator.Validate(user);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
        }
        return ResponseHandler.ReturnIActionResponse(await _authRepository.Registration(user));

    }
}
