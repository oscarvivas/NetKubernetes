using NetKubernetes.Models;

namespace NetKubernetes.Data.InterviewProgram;

public interface IInterviewRepository {
    
    Task<bool> SaveChanges();

    Task<IEnumerable<Interviewer>> GetAllInterviewer();

    Task<IEnumerable<Candidate>> GetAllCandidate();

    Task<IEnumerable<Questionary>> GetAllQuestionary();

    Task<IEnumerable<Interview>> GetAllInterview();
    
    Task<IEnumerable<Topic>> GetTopicsByQuestionary(int idQuestionary);
    
    Task<Topic> GetTopicById(int id);

    Task<Candidate> GetCandidateById(int id);

    Task<Interviewer> GetInterviewerById(int id);

    Task<Questionary> GetQuestionaryById(int id);

    Task<Interview> GetInterviewById(int id);
    
    Task<IEnumerable<Interview>> GetInterviewsByInterviewer(int idInterviewer);
    
    Task<IEnumerable<Interview>> GetInterviewsByCandidate(int idCandidate);

    Task<IEnumerable<InterviewTopic>> GetInterviewTopicByInterview(int idInterview);
    
    Task<InterviewTopic> GetInterviewTopicById(int id);

    Task CreateInterviewer(Interviewer interviewer);

    Task DeleteInterviewer(int id);

    Task CreateCandidate(Candidate candidate);

    Task DeleteCandidate(int id);

    Task CreateQuestionary(Questionary questionary);

    Task DeleteQuestionary(int id);

    Task CreateTopic(Topic topic);

    Task DeleteTopic(int id);

    Task CreateInterview(Interview interview);

    Task DeleteInterview(int id);

    Task CreateInterviewTopic(InterviewTopic interviewtopic);

    Task DeleteInterviewTopic(int id);

}