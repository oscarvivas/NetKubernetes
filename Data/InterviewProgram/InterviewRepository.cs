using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetKubernetes.Middleware;
using NetKubernetes.Models;
using NetKubernetes.Token;

namespace NetKubernetes.Data.InterviewProgram;

public class InterviewRepository : IInterviewRepository
{    
    private readonly AppDbContext _context;
    private readonly IUserSession _userSession;
    private readonly UserManager<UserApp> _userManager;

    public InterviewRepository (
        AppDbContext context,
        IUserSession userSession,
        UserManager<UserApp> userManager
    )
    {
        _context = context;
        _userSession = userSession;
        _userManager = userManager;
    }
    public async Task CreateCandidate(Candidate candidate)
    {
        var user = await _userManager.FindByNameAsync(_userSession.GetUsersession());
        if(user is null) 
        {
            throw new MiddlewareException(
                HttpStatusCode.Unauthorized,
                new { message = "El usuario no es valido para hacer esta insercion" }
            );
        }

        if(candidate is null) 
        {
            throw new MiddlewareException(
                HttpStatusCode.BadRequest,
                new { message = "Los Datos del candidato son incorrectos" }
            );
        }

        candidate.CreationDate = DateTime.Now;
        candidate.UserId = Guid.Parse(user!.Id);

        await _context.Candidates!.AddAsync(candidate);
    }

    public async Task CreateInterview(Interview interview)
    {
        var user = await _userManager.FindByNameAsync(_userSession.GetUsersession());
        if(user is null) 
        {
            throw new MiddlewareException(
                HttpStatusCode.Unauthorized,
                new { message = "El usuario no es valido para hacer esta insercion" }
            );
        }

        if(interview is null) 
        {
            throw new MiddlewareException(
                HttpStatusCode.BadRequest,
                new { message = "Los Datos de la interview son incorrectos" }
            );
        }

        interview.CreationDate = DateTime.Now;
        interview.UserId = Guid.Parse(user!.Id);

        await _context.Interviews!.AddAsync(interview);
    }

    public async Task CreateInterviewer(Interviewer interviewer)
    {
        var user = await _userManager.FindByNameAsync(_userSession.GetUsersession());
        if(user is null) 
        {
            throw new MiddlewareException(
                HttpStatusCode.Unauthorized,
                new { message = "El usuario no es valido para hacer esta insercion" }
            );
        }

        if(interviewer is null) 
        {
            throw new MiddlewareException(
                HttpStatusCode.BadRequest,
                new { message = "Los Datos de la entrevistador son incorrectos" }
            );
        }

        interviewer.CreationDate = DateTime.Now;
        interviewer.UserId = Guid.Parse(user!.Id);

        await _context.Interviewers!.AddAsync(interviewer);
    }

    public async Task CreateInterviewTopic(InterviewTopic interviewtopic)
    {
        var user = await _userManager.FindByNameAsync(_userSession.GetUsersession());
        if(user is null) 
        {
            throw new MiddlewareException(
                HttpStatusCode.Unauthorized,
                new { message = "El usuario no es valido para hacer esta insercion" }
            );
        }

        if(interviewtopic is null) 
        {
            throw new MiddlewareException(
                HttpStatusCode.BadRequest,
                new { message = "Los Datos de la interviewtopic son incorrectos" }
            );
        }

        interviewtopic.CreationDate = DateTime.Now;
        interviewtopic.UserId = Guid.Parse(user!.Id);

        await _context.InterviewTopics!.AddAsync(interviewtopic);
    }

    public async Task CreateQuestionary(Questionary questionary)
    {
        var user = await _userManager.FindByNameAsync(_userSession.GetUsersession());
        if(user is null) 
        {
            throw new MiddlewareException(
                HttpStatusCode.Unauthorized,
                new { message = "El usuario no es valido para hacer esta insercion" }
            );
        }

        if(questionary is null) 
        {
            throw new MiddlewareException(
                HttpStatusCode.BadRequest,
                new { message = "Los Datos de la interviewtopic son incorrectos" }
            );
        }

        questionary.CreationDate = DateTime.Now;
        questionary.UserId = Guid.Parse(user!.Id);

        await _context.Questionaries!.AddAsync(questionary);
    }

    public async Task CreateTopic(Topic topic)
    {
        var user = await _userManager.FindByNameAsync(_userSession.GetUsersession());
        if(user is null) 
        {
            throw new MiddlewareException(
                HttpStatusCode.Unauthorized,
                new { message = "El usuario no es valido para hacer esta insercion" }
            );
        }

        if(topic is null) 
        {
            throw new MiddlewareException(
                HttpStatusCode.BadRequest,
                new { message = "Los Datos de la interviewtopic son incorrectos" }
            );
        }

        topic.CreationDate = DateTime.Now;
        topic.UserId = Guid.Parse(user!.Id);

        await _context.Topics!.AddAsync(topic);
    }

    public async Task DeleteCandidate(int id)
    {
        var candidate = await _context.Candidates!
                        .FirstOrDefaultAsync(x => x.Id == id);

        _context.Candidates!.Remove(candidate!);
    }

    public async Task DeleteInterviewer(int id)
    {
        var interviewer = await _context.Interviewers!
                        .FirstOrDefaultAsync(x => x.Id == id);

        _context.Interviewers!.Remove(interviewer!);
    }

    public async Task DeleteInterview(int id)
    {
        var interview = await _context.Interviews!
                        .FirstOrDefaultAsync(x => x.Id == id);

        _context.Interviews!.Remove(interview!);
    }

    public async Task DeleteInterviewTopic(int id)
    {
        var interview = await _context.Interviews!
                        .FirstOrDefaultAsync(x => x.Id == id);

        _context.Interviews!.Remove(interview!);
    }

    public async Task DeleteQuestionary(int id)
    {
        var questionary = await _context.Questionaries!
                        .FirstOrDefaultAsync(x => x.Id == id);

        _context.Questionaries!.Remove(questionary!);
    }

    public async Task DeleteTopic(int id)
    {
        var topic = await _context.Topics!
                        .FirstOrDefaultAsync(x => x.Id == id);

        _context.Topics!.Remove(topic!);
    }

    public async Task<IEnumerable<Candidate>> GetAllCandidate()
    {
        return await _context.Candidates!.ToListAsync();
    }

    public async Task<IEnumerable<Interviewer>> GetAllInterviewer()
    {
        return await _context.Interviewers!.ToListAsync();
    }

    public async Task<IEnumerable<Interview>> GetAllInterview()
    {
        return await _context.Interviews!.ToListAsync();
    }

    public async Task<IEnumerable<Interview>> GetInterviewsByCandidate(int idCandidate)
    {
        return (await _context.Interviews!.Where(x => x.IdCandidate == idCandidate)!.ToListAsync())!;
    }

    public async  Task<IEnumerable<Interview>> GetInterviewsByInterviewer(int idInterviewer)
    {
        return (await _context.Interviews!.Where(x => x.IdInterviewer == idInterviewer)!.ToListAsync())!;
    }

    public async  Task<IEnumerable<InterviewTopic>> GetInterviewTopicByInterview(int idInterview)
    {
        return (await _context.InterviewTopics!.Where(x => x.IdInterview == idInterview)!.ToListAsync())!;
    }

    public async  Task<IEnumerable<Questionary>> GetAllQuestionary()
    {
        return await _context.Questionaries!.ToListAsync();
    }

    public async  Task<IEnumerable<Topic>> GetTopicsByQuestionary(int idQuestionary)
    {
        return (await _context.Topics!.Where(x => x.IdQuestionary == idQuestionary)!.ToListAsync())!;
    }

    public async  Task<InterviewTopic> GetInterviewTopicById(int id)
    {
        return (await _context.InterviewTopics!.FirstOrDefaultAsync(x => x.Id == id))!;
    }

    public async Task<Topic> GetTopicById(int id)
    {
        return (await _context.Topics!.FirstOrDefaultAsync(x => x.Id == id))!;
    }

    public async Task<Candidate> GetCandidateById(int id)
    {
        return (await _context.Candidates!.FirstOrDefaultAsync(x => x.Id == id))!;
    }

    public async Task<Interviewer> GetInterviewerById(int id)
    {
        return (await _context.Interviewers!.FirstOrDefaultAsync(x => x.Id == id))!;
    }

    public async Task<Interview> GetInterviewById(int id)
    {
        return (await _context.Interviews!.FirstOrDefaultAsync(x => x.Id == id))!;
    }

    public async Task<Questionary> GetQuestionaryById(int id)
    {
        return (await _context.Questionaries!.FirstOrDefaultAsync(x => x.Id == id))!;
    }

    public async  Task<bool> SaveChanges()
    {
        return ((await _context.SaveChangesAsync()) >= 0);
    }
}