using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using SampleUserManagement.Dtos;
using SampleUserManagement.Models;
using SampleUserManagement.Services;
using System.Text.Json;

namespace SampleUserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IDistributedCache _cache;

        public UsersController(IUserService userService, IDistributedCache cache)
        {
            _service = userService;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync([FromQuery] string? keyword)
        {
            if (String.IsNullOrEmpty(keyword))
            {
                string cacheKey = "user_list_all";
                var cachedData = await _cache.GetStringAsync(cacheKey);

                if (cachedData != null)
                {
                    var data = JsonSerializer.Deserialize<List<UserDto>>(cachedData);
                    return Ok(data);
                }
                else
                {
                    var data = _service.GetAllUsersAsync();
                    await _cache.SetStringAsync(cacheKey,JsonSerializer.Serialize(data),
                         new DistributedCacheEntryOptions
                         {
                             AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                         });
                    return Ok(data);
                }
                
            }
            else
            {
                return Ok(_service.GetUsersByNameAsync(keyword));
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto userDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Email, Fisrt Name & Last Name are required");
            }

            if (_service.GetUserByEmailAsync(userDto.Email) != null)
            {
                return Conflict($"Email {userDto.Email} already exist.");
            }

            try
            {
                await _service.CreateUserAsync(userDto);
                await _cache.RemoveAsync("user_list_all");

            }
            catch (Exception ex)
            {
                return Conflict("Failed to create user.");
            }


            return Ok($"{userDto.Email} created successfully");
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] EditUserDto userDto)
        {
            if (String.IsNullOrEmpty(userDto.Id))
            {
                return BadRequest("ID cannot be empty");
            }

            if (!String.IsNullOrEmpty(userDto.Id))
            {
                try
                {
                    if (_service.GetUserByIdAsync(userDto.Id) == null)
                    {
                        return NotFound("Id not found");

                    }
                }
                catch (Exception ex) {
                    return BadRequest("Invalid Id format");
                }
             
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Email, Fisrt Name & Last Name are required");
            }

            try
            {
                await _service.UpdateUserAsync(userDto);
                await _cache.RemoveAsync("user_list_all");

            }
            catch (Exception ex)
            {
                return Conflict("Failed to edit user.");
            }

            return Ok("User information updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest("User ID cannot be empty");
            }

            if (!String.IsNullOrEmpty(id))
            {
                try
                {
                    if (_service.GetUserByIdAsync(id) == null)
                    {
                        return NotFound("Id not found");

                    }
                }
                catch (Exception ex)
                {
                    return BadRequest("Invalid Id format");
                }

            }

            try
            {
                await _service.DeleteUserAsync(id);
                await _cache.RemoveAsync("user_list_all");
            }
            catch (Exception ex)
            {
                return Conflict("Failed to edit user.");
            }

            return Ok($"User removed from system.");
        }
    }
}
