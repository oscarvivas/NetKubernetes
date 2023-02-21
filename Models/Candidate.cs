using System.ComponentModel.DataAnnotations;

namespace NetKubernetes.Models;

public class Candidate {

    [Key]
    [Required]
    public int Id { get; set;}

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? UserId { get; set; }
}
