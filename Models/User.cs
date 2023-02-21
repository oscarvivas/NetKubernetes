using Microsoft.AspNetCore.Identity;

namespace NetKubernetes.Models;

public class UserApp : IdentityUser {
    
    public string? Name { get; set; }

    public string? LastName { get; set; }

}