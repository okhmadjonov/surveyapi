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

public class PersonService : IPersonRepository
{
    private readonly IGenericRepository<Person> _personRepository;
    public PersonService(IGenericRepository<Person> personRepository)
    {
        _personRepository = personRepository;
    }
    public async ValueTask<PersonModel> CreateAsync(PersonDto personDto)
    {
        var existPerson = await _personRepository.GetAsync(u => u.Email == personDto.Email);
     
        if (existPerson != null)
        {
            existPerson.Name = personDto.Name;
            existPerson.FirstName = personDto.FirstName;
            existPerson.LastName = personDto.LastName;
            existPerson.Email = personDto.Email;
            existPerson.Phone = personDto.Phone;
        };
        var createdPerson = await _personRepository.CreateAsync(existPerson);
        await _personRepository.SaveChangesAsync();
        return new PersonModel().MapFromEntity(createdPerson);
    }
    public async ValueTask<bool> DeleteAsync(int id)
    {
        var findPerson = await _personRepository.GetAsync(p => p.Id == id);
        if (findPerson is null)
        {
            throw new SurveyException(404, "person_not_found");
        }
        await _personRepository.DeleteAsync(id);
        await _personRepository.SaveChangesAsync();
        return true;
    }
    public async ValueTask<IEnumerable<PersonModel>> GetAll(PaginationParams @params, Expression<Func<Person, bool>> expression = null)
    {
        var persons = _personRepository.GetAll(expression: expression, isTracking: false);
        var personsList = await persons.ToPagedList(@params).ToListAsync();
        return personsList.Select(e => new PersonModel().MapFromEntity(e)).ToList();
    }
    public async ValueTask<PersonModel> GetAsync(Expression<Func<Person, bool>> expression)
    {
        var person = await _personRepository.GetAsync(expression);
        if (person is null) throw new SurveyException(404, "person_not_found");
        return new PersonModel().MapFromEntity(person);
    }

    public async ValueTask<PersonModel> UpdateAsync(int id, PersonDto personDto)
    {
        var person = await _personRepository.GetAsync(u => u.Id == id);

        if (person is null)
            throw new SurveyException(404, "person_not_found");

        if (personDto.Name != person.Name)
            person.Name = personDto.Name;

        if (personDto.FirstName != person.FirstName)
            person.FirstName = personDto.FirstName;

        if (personDto.LastName != person.LastName)
            person.LastName = personDto.LastName;

        if (personDto.Email != person.Email)
            person.Email = personDto.Email;

        if (personDto.Phone != person.Phone)
            person.Phone = personDto.Phone;

        await _personRepository.SaveChangesAsync();
        return new PersonModel().MapFromEntity(person);
    }
}
