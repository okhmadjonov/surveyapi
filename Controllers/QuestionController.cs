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
public class QuestionController : ControllerBase
{
    private readonly IQuestionRepository _questionRepository;
    public QuestionController(IQuestionRepository questionRepository)
    { _questionRepository = questionRepository; }


    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel<QuestionModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
        => ResponseHandler.ReturnResponseList(await _questionRepository.GetAll(@params));

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<QuestionModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> CreateAsync([FromForm] QuestionDto qDto)
    {
        return ResponseHandler.ReturnIActionResponse(await _questionRepository.CreateAsync(qDto));
    }


    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> DeleteAsync([FromRoute] int id)
         => ResponseHandler.ReturnIActionResponse(await _questionRepository.DeleteAsync(id));

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseModel<QuestionModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
        => ResponseHandler.ReturnIActionResponse(await _questionRepository.GetAsync(u => u.Id == id));

    [HttpPut]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<QuestionModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> UpdateAsync(int id, [FromForm] QuestionDto qDto)
        => ResponseHandler.ReturnIActionResponse(await _questionRepository.UpdateAsync(id, qDto));
}
