namespace NetKubernetes.Dtos.InterviewProgram;

public class QuestionaryResponseDto
{
    public int Id { get; set;}

    public string? Technology { get; set; }

    public List<TopicResponseDto>? Topics { set; get; }
}