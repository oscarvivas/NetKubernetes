namespace NetKubernetes.Dtos.UserDtos;

public class UserRegisterRequestDto 
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}