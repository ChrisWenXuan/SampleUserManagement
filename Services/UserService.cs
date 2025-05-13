using SampleUserManagement.Data;
using SampleUserManagement.Dtos;
using SampleUserManagement.Models;

namespace SampleUserManagement.Services
{
    public class UserService : IUserService
    {
        private readonly UserManagementDbContext _context;
        private readonly ILogger<UserService> _logger;


        public UserService(UserManagementDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task CreateUserAsync(CreateUserDto user)
        {
               var result = _context.Users.Add(new User
                {
                    UserId = GenerateNewUserId(),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email.ToLower(),
                    Status = 1,
                    CreatedAt = DateTime.Now,
                });

                await _context.SaveChangesAsync();

            // publish message to broker 
            _logger.LogInformation($"User created: {result.Entity.Email} ({result.Entity.UserId})");  

        }

        public async Task DeleteUserAsync(string id)
        {
            User u = _context.Users.FirstOrDefault(x => x.Id.Equals(Guid.Parse(id)));
            if (u != null)
            {
                u.Status = -1;
                u.LastModifiedAt = DateTime.Now;

                _context.Update(u);
                await _context.SaveChangesAsync();

                // publish message to broker 
                _logger.LogInformation($"User deleted.");
            }
        }

        public List<UserDto> GetAllUsersAsync()
        {
            return ConvertToUserDtoList(
                _context.Users.Where(x=>x.Status.Equals(1))
                .OrderByDescending(x=> x.CreatedAt).Take(1000).ToList());
        }

        public UserDto GetUserByEmailAsync(string email)
        {
            User? user = _context.Users.FirstOrDefault(x => x.Email.ToLower().Equals(email.ToLower()) && x.Status.Equals(1));

            if (user == null)
            {
                return null;
            }
            return ConvertToUserDto(user);
        }

        public UserDto GetUserByUserIdAsync(string userid)
        {
            User? user = _context.Users.FirstOrDefault(x => x.UserId.ToLower().Equals(userid.ToLower()));

            if (user == null)
            {
                return null;
            }
            return ConvertToUserDto(user);
        }

        public UserDto GetUserByIdAsync(string id)
        {
            User? user = _context.Users.FirstOrDefault(x => x.Id.Equals(Guid.Parse(id)));

            if (user == null)
            {
                return null;
            }
            return ConvertToUserDto(user);
        }

        public List<UserDto> GetUsersByNameAsync(string keyword)
        {
            return ConvertToUserDtoList(
                _context.Users.Where(
                    x=> (x.FirstName.Contains(keyword) || x.LastName.Contains(keyword)) && x.Status.Equals(1))
                .OrderByDescending(x => x.CreatedAt).ToList());
        }

        public async Task UpdateUserAsync(EditUserDto user)
        {
            User u = _context.Users.FirstOrDefault(x => x.Id.Equals(Guid.Parse(user.Id)));
            if (u != null) {

                u.FirstName = user.FirstName;
                u.LastName = user.LastName;
                u.LastModifiedAt = DateTime.Now;

                _context.Update(u);
                await _context.SaveChangesAsync();

                // publish message to broker 
                _logger.LogInformation($"User Updated");


            }


        }

        private string GenerateNewUserId()
        {
            // sample format : UID00007
            return $"UID{_context.Users.Count() + 1:D5}";
        }

        private UserDto ConvertToUserDto(User user) {
            return new UserDto {
                Id = user.Id.ToString(),
                UserId = user.UserId, 
                FirstName = user.FirstName,
                LastName = user.LastName, 
                Email = user.Email };
        }

        private List<UserDto> ConvertToUserDtoList(List<User> user)
        {
            
            return user.Select(x=> new UserDto
            {
                Id = x.Id.ToString(),
                UserId = x.UserId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email
            }).ToList();
        }
    }
}
