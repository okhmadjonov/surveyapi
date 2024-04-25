﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using surveyapi.Configuration;
using surveyapi.Dtos.User;
using surveyapi.Extentions;
using surveyapi.FluentValidation;
using surveyapi.Models;
using surveyapi.Repositories;

namespace surveyapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class UsersController : ControllerBase
{
    protected readonly IUserRepository _userRepository;
    protected readonly IAuthRepository _authRepository;
    public UsersController(IAuthRepository authRepository, IUserRepository userRepository)
    {
        _authRepository = authRepository;
        _userRepository = userRepository;

    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel<UserModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
       => ResponseHandler.ReturnResponseList(await _userRepository.GetAll(@params));



    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<UserModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
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

    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> DeleteAsync([FromRoute] int id)
         => ResponseHandler.ReturnIActionResponse(await _userRepository.DeleteAsync(id));

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseModel<UserModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
        => ResponseHandler.ReturnIActionResponse(await _userRepository.GetAsync(u => u.Id == id));

    [HttpPut]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<UserModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> UpdateAsync(int id, UserForCreationDto userForUpdateDTO)
    {
        var validator = new RegisterValidator();
        var validationResult = validator.Validate(userForUpdateDTO);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
        }
        return ResponseHandler.ReturnIActionResponse(await _userRepository.UpdateAsync(id, userForUpdateDTO));

    }
}