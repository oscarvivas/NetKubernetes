namespace NetKubernetes.Dtos.InterviewProgram;

public class InterviewResponseDto 
{
    public int Id { get; set;}

    public int IdInterviewer { get; set;}

    public int IdCandidate { get; set;}

    public string? Grade { get; set; }

    public string? GrowthPotential { get; set; }
}
