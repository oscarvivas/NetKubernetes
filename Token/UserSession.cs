using System.Security.Claims;
namespace NetKubernetes.Token;

public class UserSession : IUserSession {

    private readonly IHttpContextAccessor _httpContextAccesor;

    public UserSession(IHttpContextAccessor httpContextAccesor) {
        _httpContextAccesor = httpContextAccesor;
    }

    public string GetUsersession() 
    {
        var userName = _httpContextAccesor.HttpContext!.User?.Claims?
                        .FirstOrDefault(x=>x.Type == ClaimTypes.NameIdentifier)?.Value;
        return userName!;
    }
    
}