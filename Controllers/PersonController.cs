using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using surveyapi.Configuration;
using surveyapi.Dtos;
using surveyapi.Extentions;
using surveyapi.Models;
using surveyapi.Repositories;

namespace surveyapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPersonRepository _personRepository;
    public PersonController(IPersonRepository personRepository)
    { _personRepository = personRepository; }


    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel<PersonModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
        => ResponseHandler.ReturnResponseList(await _personRepository.GetAll(@params));

    [HttpPost]
    [ProducesResponseType(typeof(ResponseModel<PersonModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> CreateAsync([FromBody] PersonDto pDto)
    {
        return ResponseHandler.ReturnIActionResponse(await _personRepository.CreateAsync(pDto));
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ResponseModel<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> DeleteAsync([FromRoute] int id)
         => ResponseHandler.ReturnIActionResponse(await _personRepository.DeleteAsync(id));

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseModel<PersonModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
        => ResponseHandler.ReturnIActionResponse(await _personRepository.GetAsync(u => u.Id == id));

    [HttpPut]
    [ProducesResponseType(typeof(ResponseModel<PersonModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> UpdateAsync(int id, [FromBody] PersonDto pDto)
        => ResponseHandler.ReturnIActionResponse(await _personRepository.UpdateAsync(id, pDto));
}
