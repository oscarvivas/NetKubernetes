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
public class CandidateController : ControllerBase  
{
    private readonly IInterviewRepository _repository;
    private IMapper _mapper;

    public CandidateController (
        IInterviewRepository repository,
        IMapper mapper
    )
    {
        _mapper = mapper;
        _repository = repository;
    }    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CandidateResponseDto>>> GetCandidates()
    {
        var result = await _repository.GetAllCandidate();
        return Ok(_mapper.Map<IEnumerable<CandidateResponseDto>>(result));
    }

    [HttpGet("{id}", Name = "GetCandidateById")]
    public async Task<ActionResult<CandidateResponseDto>> GetCandidateById(int id)
    {
        var result = await _repository.GetCandidateById(id);
        if(result is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.NotFound,
                new { message = $"No se encontro el Candidato por este id {id}" }
            );
        }
        return Ok(_mapper.Map<CandidateResponseDto>(result));
    }

    [HttpPost]
    public async Task<ActionResult<CandidateResponseDto>> CreateCandidate(
        [FromBody] CandidateRequestDto candidate
    )
    {
        var candidateModel = _mapper.Map<Candidate>(candidate);
        await _repository.CreateCandidate(candidateModel);
        await _repository.SaveChanges();

        var candidateResponse = _mapper.Map<CandidateResponseDto>(candidateModel);

        return CreatedAtRoute(nameof(GetCandidateById), new { candidateResponse.Id}, candidateResponse);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCandidate(int id) {
        await _repository.DeleteCandidate(id);
        await _repository.SaveChanges();
        return Ok();
    }
}