using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetKubernetes.Dtos.UserDtos;
using NetKubernetes.Middleware;
using NetKubernetes.Token;
using NetKubernetes.Models;

namespace NetKubernetes.Data.Users;

public class UserRepository : IUserRepository
{
    private readonly UserManager<UserApp> _userManager;
    private readonly SignInManager<UserApp> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly AppDbContext _context;
    private readonly IUserSession _usersession;

    public UserRepository(
        UserManager<UserApp> userManager,
        SignInManager<UserApp> signInManager,
        IJwtGenerator jwtGenerator,
        AppDbContext context,
        IUserSession usersession
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
        _context = context;
        _usersession = usersession;
    }

    private UserResponseDto TransformerUserToUserDto(UserApp user){
        return new UserResponseDto {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            UserName = user.UserName,
            Token = _jwtGenerator.CreateToken(user)
        };
    }

    public async Task<UserResponseDto> GetUser()
    {
        var user = await _userManager.FindByNameAsync(_usersession.GetUsersession());
        if(user is null) {
            throw new MiddlewareException(
                HttpStatusCode.Unauthorized, 
                new { message = "El usuario del token no existe en la base de datos"} 
            );
        }

        var userDto = TransformerUserToUserDto(user!);
        return userDto;
    }

    public async Task<UserResponseDto> Login(UserLoginRequestDto request)
    {
        var user =  await _userManager.FindByEmailAsync(request.Email!);
        if(user is null) {
            throw new MiddlewareException(
                HttpStatusCode.Unauthorized, 
                new { message = "El email del usuario no existe en la base de datos"} 
            );
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user!, request.Password!, false);
        if(result.Succeeded) 
        {
            return TransformerUserToUserDto(user!);
        } 

        throw new MiddlewareException(
            HttpStatusCode.Unauthorized, 
            new { message = "Las credenciales son incorrectas"} 
        );
    }

    public async Task<UserResponseDto> RegisterUser(UserRegisterRequestDto request)
    {
        var existsEmail = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();
        if(existsEmail)
        {
            throw new MiddlewareException(
                HttpStatusCode.BadRequest, 
                new { message = "El email del usuario ya existe en la base de datos" } 
            );
        }

        var existsUserName = await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync();
        if(existsUserName)
        {
            throw new MiddlewareException(
                HttpStatusCode.BadRequest, 
                new { message = "El nombre del usuario ya existe en la base de datos" } 
            );
        }

        var user = new UserApp {
            Name = request.Name,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            UserName = request.UserName
        };
        var result = await _userManager.CreateAsync(user!, request.Password!);
        if(result.Succeeded)
        {
            return TransformerUserToUserDto(user);
        }

        string errorMessage = string.Join(" - " , result.Errors.Select(s => s.Description));
        throw new Exception("No se pudo registrar el usuario " + " - " + errorMessage);
    }
}
