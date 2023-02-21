using System.ComponentModel.DataAnnotations;

namespace NetKubernetes.Models;

public class Topic {

    [Key]
    [Required]
    public int Id { get; set;}

    [Required]
    public int IdQuestionary { get; set;}

    public string? TopicName { get; set; }

    public string? Description { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? UserId { get; set; }
}

