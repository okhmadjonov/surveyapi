using Microsoft.EntityFrameworkCore;
using surveyapi.Configuration;
using surveyapi.Dtos;
using surveyapi.Entities;
using surveyapi.Exceptions;
using surveyapi.Extentions;
using surveyapi.Models;
using surveyapi.Repositories;
using System.Linq.Expressions;

namespace surveyapi.Services;

public class ChoiceService : IChoiceRepository
{

    private readonly IGenericRepository<Choice> _choiceRepository;

    public ChoiceService(IGenericRepository<Choice> choiceRepository)
    {
        _choiceRepository = choiceRepository;
    }
    public async ValueTask<ChoiceModel> CreateAsync(ChoiceDto chDto)
    {
        if (chDto == null)
        {
            throw new SurveyException(404, "choice_cannot_be_empty");
        }

        var choice = new Choice
        {
            Text = chDto.Text,
            IsSelected = chDto.IsSelected,
            QuestionId = chDto.QuestionId,

        };
        var createdChoice = await _choiceRepository.CreateAsync(choice);
        await _choiceRepository.SaveChangesAsync();
        return new ChoiceModel().MapFromEntity(createdChoice);
    }

    public async ValueTask<bool> DeleteAsync(int id)
    {
        var findChoice = await _choiceRepository.GetAsync(p => p.Id == id);
        if (findChoice is null)
        {
            throw new SurveyException(404, "choice_not_found");
        }
        await _choiceRepository.DeleteAsync(id);
        await _choiceRepository.SaveChangesAsync();
        return true;
    }

    public async ValueTask<IEnumerable<ChoiceModel>> GetAll(PaginationParams @params, Expression<Func<Choice, bool>> expression = null)
    {
        var choices = _choiceRepository.GetAll(expression: expression, isTracking: false);
        var choicesList = await choices.ToPagedList(@params).ToListAsync();
        return choicesList.Select(e => new ChoiceModel().MapFromEntity(e)).ToList();
    }

    public async ValueTask<ChoiceModel> GetAsync(Expression<Func<Choice, bool>> expression)
    {
        var choice = await _choiceRepository.GetAsync(expression, false);
        if (choice is null)
            throw new SurveyException(404, "choice_not_found");
        return new ChoiceModel().MapFromEntity(choice);
    }

    public async ValueTask<ChoiceModel> UpdateAsync(int id, ChoiceDto chDto)
    {
        var existingChoice = await _choiceRepository.GetAsync(p => p.Id == id);

        if (existingChoice == null)
        {
            throw new SurveyException(404, "choice_not_found");
        }

        if (chDto.Text != null)
        {
            existingChoice.Text = chDto.Text;
        }

        if (chDto.IsSelected)
        {
            existingChoice.IsSelected = chDto.IsSelected;
        }

        if ( chDto.QuestionId != null)
        {
            existingChoice.QuestionId= chDto.QuestionId;
        }
        await _choiceRepository.SaveChangesAsync();
        return new ChoiceModel().MapFromEntity(existingChoice);
    }
}
