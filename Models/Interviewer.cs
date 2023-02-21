using System.ComponentModel.DataAnnotations;

namespace NetKubernetes.Models;

public class Interviewer {

    [Key]
    [Required]
    public int Id { get; set;}

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Picture { get; set; }

    public string? DU { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? UserId { get; set; }
}

