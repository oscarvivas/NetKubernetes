using System.Net;
using Microsoft.AspNetCore.Identity;
using NetKubernetes.Middleware;
using NetKubernetes.Token;
using NetKubernetes.Data;
using NetKubernetes.Models;
using Microsoft.EntityFrameworkCore;

namespace NetKubernetes.Data.Inmuebles;

public class InmuebleRepository : IInmuebleRepository
{
    private readonly AppDbContext _context;
    private readonly IUserSession _userSession;
    private readonly UserManager<UserApp> _userManager;

    public InmuebleRepository (
        AppDbContext context,
        IUserSession userSession,
        UserManager<UserApp> userManager
    )
    {
        _context = context;
        _userSession = userSession;
        _userManager = userManager;
    }

    public async Task CreateInmueble(Inmueble inmueble)
    {
        var user = await _userManager.FindByNameAsync(_userSession.GetUsersession());
        if(user is null) 
        {
            throw new MiddlewareException(
                HttpStatusCode.Unauthorized,
                new { message = "El usuario no es valido para hacer esta insercion" }
            );
        }

        if(inmueble is null) 
        {
            throw new MiddlewareException(
                HttpStatusCode.BadRequest,
                new { message = "Los Datos del inmuble son incorrectos" }
            );
        }

        inmueble.CreationDate = DateTime.Now;
        inmueble.UserId = Guid.Parse(user!.Id);

        await _context.Inmuebles!.AddAsync(inmueble);
    }

    public async Task DeleteInmueble(int id)
    {
        var inmueble = await _context.Inmuebles!
                        .FirstOrDefaultAsync(x => x.Id == id);

        _context.Inmuebles!.Remove(inmueble!);
    }

    public async Task<IEnumerable<Inmueble>> GetAllInmuebles()
    {
        return await _context.Inmuebles!.ToListAsync();
    }

    public async Task<Inmueble> GetInmuebleById(int id)
    {
        return (await _context.Inmuebles!.FirstOrDefaultAsync(x => x.Id == id))!;
    }

    public async Task<bool> SaveChanges()
    {
        return ((await _context.SaveChangesAsync()) >= 0);
    }
}