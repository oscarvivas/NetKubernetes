namespace NetKubernetes.Dtos.InmuebleDtos;

public class InmuebleResponseDto {
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; } 

    public decimal Price { get; set; }

    public string? Picture { get; set; }

    public DateTime? CreationDate { get; set; }

}