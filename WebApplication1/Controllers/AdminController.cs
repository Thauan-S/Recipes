using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(Policy ="RoleAdmin")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userManager.Users.Select(u => new { u.Id, u.Email }).ToListAsync();

            return Ok(users);
        }
        [HttpPost("users")]
        public async Task<IActionResult> AddUser([FromBody] ClientDto clientDto)
        {

            if (!isValidEmail(clientDto.Email))
            {
                return BadRequest(new { message = "Invalid Email Format" });
            }
            var existingUser = await userManager.FindByEmailAsync(clientDto.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "Email already exists" });
            }
            var user = new IdentityUser { Email = clientDto.Email, UserName = clientDto.Email };
            var result = await userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync("Basic"))
                {
                    var roleResult = await roleManager.CreateAsync(new IdentityRole("Basic"));
                    if (roleResult.Succeeded)
                    {
                        await userManager.DeleteAsync(user);
                        return StatusCode(500, new { message = "User role Creation Failed", errors = roleResult.Errors });
                    }
                }
                await userManager.AddToRoleAsync(user, "Basic");
                return Ok(new { message = "User Added Succesfully" });
            }
            return BadRequest(new { message = result.Errors });
        }
        [HttpDelete("users")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest(new { message = result.Errors });
        }
        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await roleManager.Roles.Select(r => new { r.Id, r.Name }).ToListAsync();
            if (roles == null)
            {
                return NoContent();
            }
            return Ok(roles);
        }
        [HttpPost("roles")]
        public async Task<IActionResult> AddNewRole([FromBody] string roleName)
        {
            if (await roleManager.RoleExistsAsync(roleName))
            {
                return BadRequest(new { roleName = "Role already exists" });
            }
            var result = await roleManager.CreateAsync(new IdentityRole { Name = roleName });
            if (result.Succeeded)
            {
                return Ok(new { message = "Role added successfully" });
            }
            return BadRequest(result.Errors);
        }
        [HttpDelete("roles")]
        public async Task<IActionResult> DeleteRole([FromBody] string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return BadRequest(new { roleName = "Role not exists" });
            }
            if (role.Name == "Admin")
            {
                return BadRequest(new { roleName = "Admin role cannot be deleted" });
            }
            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return Ok(new { message = "Role deleted successfully" });
            }
            return BadRequest(result.Errors);
        }
        [HttpPost("change-user-role")]
        public async Task<IActionResult> ChangeUserRole([FromBody] ChangeRole changeRole)
        {
            var user = await userManager.FindByEmailAsync(changeRole.UserEmail);
            if (user == null)
            {
                return NotFound(new { message = $"User with email {changeRole.UserEmail} not found" });
            }
            if (!await roleManager.RoleExistsAsync(changeRole.NewRole))
            {
                return BadRequest($"Role {changeRole.NewRole} does not exists");
            }
            var currentRoles = await userManager.GetRolesAsync(user);
            var removeResult = await userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                return BadRequest("Failed to remove user´s current role");
            }
            var addResult = await userManager.AddToRoleAsync(user, changeRole.NewRole);
            if (addResult.Succeeded)
            {
                return Ok(new { message = $"User {changeRole.UserEmail} role changes to {changeRole.NewRole}  successfully" });
            }
            return BadRequest("Failed to add user to new role");
        }
        [HttpPut("admin-info")]
        public async Task<IActionResult> UpdateAdminInfo([FromBody] UpdateUserInfo updateUser)
        {
            var admin = await userManager.FindByEmailAsync(updateUser.Email);
            if (admin == null)
            {
                return NotFound();
            }
            admin.UserName = updateUser.UserName;
            admin.Email = updateUser.Email;
            var result = await userManager.UpdateAsync(admin);
            if (result.Succeeded)
            {
                return Ok(new { message = "Admin info updated successfully" });
            }
            return BadRequest(result.Errors);
        }

        [HttpPut("change-admin-password")]
        public async Task<IActionResult> ChangeAdminPassword([FromBody] ChangePassword changePassword)
        {
            var admin = await userManager.FindByEmailAsync(changePassword.Email);

            if (admin == null)
            {
                return NotFound();
            }


            var result = await userManager.ChangePasswordAsync(admin, changePassword.CurrentPassword, changePassword.NewPassword);
            if (result.Succeeded)
            {
                return Ok(new { message = "Password has been modified" });
            }
            return BadRequest(result.Errors);
        }
        private bool isValidEmail(string email)
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email);
                return address.Address == email;
            }
            catch { return false; }


        }
    }
}
