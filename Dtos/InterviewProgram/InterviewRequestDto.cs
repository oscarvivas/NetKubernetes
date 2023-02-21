namespace NetKubernetes.Dtos.InterviewProgram;

public class InterviewRequestDto 
{
    public int IdInterviewer { get; set;}

    public int IdCandidate { get; set;}

    public string? Grade { get; set; }

    public string? GrowthPotential { get; set; }
}
