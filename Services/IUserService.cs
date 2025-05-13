using SampleUserManagement.Dtos;

namespace SampleUserManagement.Services
{
    public interface IUserService
    {
        List<UserDto> GetAllUsersAsync();
        List<UserDto> GetUsersByNameAsync(string keyword);
        UserDto GetUserByUserIdAsync(string userid);
        UserDto GetUserByIdAsync(string id);
        UserDto GetUserByEmailAsync(string email);

        Task CreateUserAsync(CreateUserDto user);
        Task UpdateUserAsync(EditUserDto user);
        Task DeleteUserAsync(string userid);
    }
}
