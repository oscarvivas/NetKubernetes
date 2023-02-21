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
public class InterviewController : ControllerBase  
{
    private readonly IInterviewRepository _repository;
    private IMapper _mapper;

    public InterviewController (
        IInterviewRepository repository,
        IMapper mapper
    )
    {
        _mapper = mapper;
        _repository = repository;
    }    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InterviewResponseDto>>> GetInterviews()
    {
        var result = await _repository.GetAllInterview();
        return Ok(_mapper.Map<IEnumerable<InterviewResponseDto>>(result));
    }

    [HttpGet("{id}", Name = "GetInterviewById")]
    public async Task<ActionResult<InterviewResponseDto>> GetInterviewById(int id)
    {
        var result = await _repository.GetInterviewById(id);
        if(result is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.NotFound,
                new { message = $"No se encontro entrevistas por el id {id}" }
            );
        }
        return Ok(_mapper.Map<InterviewResponseDto>(result));
    }

    [HttpGet("{idCandidate}", Name = "GetInterviewsByCandidate")]
    public async Task<ActionResult<InterviewResponseDto>> GetInterviewsByCandidate(int idCandidate)
    {
        var result = await _repository.GetInterviewsByCandidate(idCandidate);
        if(result is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.NotFound,
                new { message = $"No se encontro entrevistas para el candidado id {idCandidate}" }
            );
        }
        return Ok(_mapper.Map<InterviewResponseDto>(result));
    }

    [HttpGet("{idInterviewer}", Name = "GetInterviewsByInterviewer")]
    public async Task<ActionResult<InterviewResponseDto>> GetInterviewsByInterviewer(int idInterviewer)
    {
        var result = await _repository.GetInterviewsByInterviewer(idInterviewer);
        if(result is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.NotFound,
                new { message = $"No se encontro entrevistas para el entrevistador id {idInterviewer}" }
            );
        }
        return Ok(_mapper.Map<InterviewResponseDto>(result));
    }

    [HttpPost]
    public async Task<ActionResult<InterviewResponseDto>> CreateInterview(
        [FromBody] InterviewRequestDto interview
    )
    {
        var InterviewModel = _mapper.Map<Interview>(interview);
        await _repository.CreateInterview(InterviewModel);
        await _repository.SaveChanges();

        var InterviewResponse = _mapper.Map<InterviewResponseDto>(InterviewModel);

        return CreatedAtRoute(nameof(GetInterviewById), new { InterviewResponse.Id}, InterviewResponse);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteInterview(int id) {
        await _repository.DeleteInterview(id);
        await _repository.SaveChanges();
        return Ok();
    }
}