using System.ComponentModel.DataAnnotations;

namespace NetKubernetes.Models;

public class Interview {

    [Key]
    [Required]
    public int Id { get; set;}

    [Required]
    public int IdInterviewer { get; set;}

    [Required]
    public int IdCandidate { get; set;}

    public string? Grade { get; set; }

    public string? GrowthPotential { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? UserId { get; set; }
}

