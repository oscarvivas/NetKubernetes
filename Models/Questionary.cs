using System.ComponentModel.DataAnnotations;

namespace NetKubernetes.Models;

public class Questionary {

    [Key]
    [Required]
    public int Id { get; set;}

    public string? Technology { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? UserId { get; set; }
}

