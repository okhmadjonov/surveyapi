using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using surveyapi.Configuration;
using surveyapi.Dtos;
using surveyapi.Extentions;
using surveyapi.Models;
using surveyapi.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace surveyapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SurveyController : ControllerBase
{
    private readonly ISurveyRepository _surveyRepository;
    public SurveyController(ISurveyRepository surveyRepository)
    {
        _surveyRepository = surveyRepository; 
    
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel<SurveyModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
        => ResponseHandler.ReturnResponseList(await _surveyRepository.GetAll(@params));

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<QuestionModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> CreateAsync([FromBody] SurveyDto sDto, int UserId)
    {
        return ResponseHandler.ReturnIActionResponse(await _surveyRepository.CreateAsync(sDto, UserId));
    }

    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> DeleteAsync([FromRoute] int id)
         => ResponseHandler.ReturnIActionResponse(await _surveyRepository.DeleteAsync(id));

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseModel<SurveyModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
        => ResponseHandler.ReturnIActionResponse(await _surveyRepository.GetAsync(u => u.Id == id));

    [HttpPut]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<SurveyModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> UpdateAsync(int id, [FromForm] SurveyDto sDto)
        => ResponseHandler.ReturnIActionResponse(await _surveyRepository.UpdateAsync(id, sDto));

    

    [HttpGet("usersurvey")]
    [ProducesResponseType(typeof(ResponseModel<SurveyModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Return all surveys belongs to individual user", Description = "To get surveys by user email")]
    public async ValueTask<IActionResult> GetSurveysByUserEmail(string email)
    { 
        return ResponseHandler.ReturnResponseList(await _surveyRepository.GetSurveysByUserEmail(email));
    }
}
