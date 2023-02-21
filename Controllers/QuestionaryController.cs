using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetKubernetes.Data.InterviewProgram;
using NetKubernetes.Dtos.InterviewProgram;
using NetKubernetes.Middleware;
using NetKubernetes.Models;

namespace NetKubernetes.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionaryController : ControllerBase  
{
    private readonly IInterviewRepository _repository;
    private IMapper _mapper;

    public QuestionaryController (
        IInterviewRepository repository,
        IMapper mapper
    )
    {
        _mapper = mapper;
        _repository = repository;
    }    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuestionaryResponseDto>>> GetQuestionaries()
    {
        var result = await _repository.GetAllQuestionary();
        return Ok(_mapper.Map<IEnumerable<QuestionaryResponseDto>>(result));
    }

    [HttpGet("{id}", Name = "GetQuestionaryById")]
    public async Task<ActionResult<QuestionaryResponseDto>> GetQuestionaryById(int id)
    {
        var result = await _repository.GetQuestionaryById(id);
        if(result is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.NotFound,
                new { message = $"No se encontro el entrevistador por este id {id}" }
            );
        }
        return Ok(_mapper.Map<QuestionaryResponseDto>(result));
    }

    [HttpPost]
    public async Task<ActionResult<QuestionaryResponseDto>> CreateQuestionary(
        [FromBody] QuestionaryRequestDto Questionary
    )
    {
        var questionaryModel = _mapper.Map<Questionary>(Questionary);
        await _repository.CreateQuestionary(questionaryModel);
        await _repository.SaveChanges();

        var QuestionaryResponse = _mapper.Map<QuestionaryResponseDto>(questionaryModel);

        return CreatedAtRoute(nameof(GetQuestionaryById), new { QuestionaryResponse.Id}, QuestionaryResponse);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteQuestionary(int id) {
        await _repository.DeleteQuestionary(id);
        await _repository.SaveChanges();
        return Ok();
    }
}