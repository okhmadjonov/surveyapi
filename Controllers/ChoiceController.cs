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
public class ChoiceController : ControllerBase
{
    private readonly IChoiceRepository _choiceRepository;
    public ChoiceController(IChoiceRepository choiceRepository)
    { _choiceRepository = choiceRepository; }


    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel<ChoiceModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
        => ResponseHandler.ReturnResponseList(await _choiceRepository.GetAll(@params));

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<ChoiceModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> CreateAsync([FromForm] ChoiceDto chDto)
    {
        return ResponseHandler.ReturnIActionResponse(await _choiceRepository.CreateAsync(chDto));
    }


    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> DeleteAsync([FromRoute] int id)
         => ResponseHandler.ReturnIActionResponse(await _choiceRepository.DeleteAsync(id));

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseModel<ChoiceModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
        => ResponseHandler.ReturnIActionResponse(await _choiceRepository.GetAsync(u => u.Id == id));

    [HttpPut]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<ChoiceModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> UpdateAsync(int id, [FromForm] ChoiceDto chDto)
        => ResponseHandler.ReturnIActionResponse(await _choiceRepository.UpdateAsync(id, chDto));
}
