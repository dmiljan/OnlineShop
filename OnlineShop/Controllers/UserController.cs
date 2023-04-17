using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserController> _logger;

        public UserController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<UserController> logger
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        [HttpGet("getRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return Ok(roles);
        }

        [HttpPost("createRole")]
        public async Task<IActionResult> CreateRole(string name)
        {
            var roleExist = await _roleManager.RoleExistsAsync(name);

            if (!roleExist)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(name));

                if (roleResult.Succeeded)
                {
                    _logger.LogInformation($"The Role {name} has been added successfully");
                    return Ok(new
                    {
                        result = $"The role {name} has been added sucessfully"
                    });
                }
                else
                {
                    _logger.LogInformation($"The Role {name} has not been added successfully");
                    return BadRequest(new
                    {
                        error = $"The role {name} has not been added sucessfully"
                    });
                }
            }
            return BadRequest(new { error = "Role already exist" });
        }

        [HttpGet("getUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _logger.LogInformation($"The user with email {email} does not exist");
                return BadRequest(new
                {
                    error = "User does not exist"
                });
            }

            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExist)
            {
                _logger.LogInformation($"The role {roleName} does not exist");
                return BadRequest(new
                {
                    error = "Role does not exist"
                });
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return Ok(new
                {
                    result = "Success, user has been added to the role"
                });

            }
            else
            {
                _logger.LogInformation($"The user was not abel to be added to the role");
                return BadRequest(new
                {
                    error = "The user was not abel to be added to the role"
                });
            }
        }

        [HttpGet("GetAllUserRoles")]
        public async Task<IActionResult> GetAllUserRoles(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _logger.LogInformation($"The user with email {email} does not exist");
                return BadRequest(new
                {
                    error = "User does not exist"
                });
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(roles);
        }

        [HttpPost("RemoveUserFromRole")]
        public async Task<IActionResult> RemoveUserFromRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _logger.LogInformation($"The user with email {email} does not exist");
                return BadRequest(new
                {
                    error = "User does not exist"
                });
            }

            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExist)
            {
                _logger.LogInformation($"The role {roleName} does not exist");
                return BadRequest(new
                {
                    error = "Role does not exist"
                });
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                return Ok(new
                {
                    result = $"User {email} has been removed from role {roleName}"
                });
            }

            return BadRequest(new
            {
                error = $"Unable to remove User {email} from role {roleName}"
            });
        }
    }
}
