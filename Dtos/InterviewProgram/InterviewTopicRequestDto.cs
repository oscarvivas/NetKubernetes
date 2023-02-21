namespace NetKubernetes.Dtos.InterviewProgram;

public class InterviewTopicRequestDto 
{
    public int Id { get; set;}

    public int IdInterview { get; set;}

    public int IdTopic { get; set;}

    public string? Score { get; set; }

    public string? Notes { get; set; }
}
