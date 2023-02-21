using NetKubernetes.Dtos.UserDtos;

namespace NetKubernetes.Data.Users; 

public interface IUserRepository {
    Task<UserResponseDto> GetUser();
    Task<UserResponseDto> Login(UserLoginRequestDto request);
    Task<UserResponseDto> RegisterUser(UserRegisterRequestDto request);
}
