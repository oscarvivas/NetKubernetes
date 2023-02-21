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
public class InterviewerController : ControllerBase  
{
    private readonly IInterviewRepository _repository;
    private IMapper _mapper;

    public InterviewerController (
        IInterviewRepository repository,
        IMapper mapper
    )
    {
        _mapper = mapper;
        _repository = repository;
    }    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InterviewerResponseDto>>> GetInterviewers()
    {
        var result = await _repository.GetAllInterviewer();
        return Ok(_mapper.Map<IEnumerable<InterviewerResponseDto>>(result));
    }

    [HttpGet("{id}", Name = "GetInterviewerById")]
    public async Task<ActionResult<InterviewerResponseDto>> GetInterviewerById(int id)
    {
        var result = await _repository.GetInterviewerById(id);
        if(result is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.NotFound,
                new { message = $"No se encontro el entrevistador por este id {id}" }
            );
        }
        return Ok(_mapper.Map<InterviewerResponseDto>(result));
    }

    [HttpPost]
    public async Task<ActionResult<InterviewerResponseDto>> CreateInterviewer(
        [FromBody] InterviewerRequestDto interviewer
    )
    {
        var interviewerModel = _mapper.Map<Interviewer>(interviewer);
        await _repository.CreateInterviewer(interviewerModel);
        await _repository.SaveChanges();

        var InterviewerResponse = _mapper.Map<InterviewerResponseDto>(interviewerModel);

        return CreatedAtRoute(nameof(GetInterviewerById), new { InterviewerResponse.Id}, InterviewerResponse);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteInterviewer(int id) {
        await _repository.DeleteInterviewer(id);
        await _repository.SaveChanges();
        return Ok();
    }
}