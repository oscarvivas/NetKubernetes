using System.ComponentModel.DataAnnotations;

namespace NetKubernetes.Models;

public class InterviewTopic {

    [Key]
    [Required]
    public int Id { get; set;}

    [Required]
    public int IdInterview { get; set;}

    [Required]
    public int IdTopic { get; set;}

    public string? Score { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? UserId { get; set; }
}

