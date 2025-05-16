//using System.Security.Claims;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using WebApplication1.Models;

//namespace WebApplication1.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize(Policy = "RoleUser")]
//    public class BasicController : ControllerBase
//    {
//        private readonly UserManager<IdentityUser> userManager;

//        public BasicController(UserManager<IdentityUser> userManager)
//        {
//            this.userManager = userManager;
//        }

//        [HttpGet("get")]
//        public IActionResult GetUserInfo()
//        {
//            return Ok(new { message = "ola" });

//        }
//        [HttpGet("user-info")]
//        public async Task<IActionResult> GetUserInfo([FromBody] string email)
//        {
//            var user = await userManager.FindByIdAsync(email);
//            if (user == null)
//            {
//                return NotFound("User not found");
//            }
//            return Ok(new { user.Id, user.Email, user.UserName, user.PhoneNumber });
//        }
//        [HttpPut("user-info")]
//        public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserInfo updateUserInfo)
//        {
//            var user = await userManager.FindByEmailAsync(updateUserInfo.Email);
//            if (user == null)
//            {
//                return NotFound("User not found");
//            }
//            user.UserName = updateUserInfo.UserName;
//            user.Email = updateUserInfo.Email;
//            user.PhoneNumber = updateUserInfo.PhoneNumber;
//            var result = await userManager.UpdateAsync(user);
//            if (result.Succeeded)
//            {
//                return Ok(new { message = "User Info Updated Successfully" });
//            }
//            return BadRequest(result.Errors);
//        }
//        [HttpDelete("delete-user")]
//        public async Task<IActionResult> DeleteUser([FromBody] string email)
//        {
//            var user = await userManager.FindByEmailAsync(email);
//            if (user == null)
//            {
//                return NotFound("User not found");
//            }
//            var result = await userManager.DeleteAsync(user);
//            if (result.Succeeded)
//            {
//                return NoContent();
//            }
//            return BadRequest(result.Errors);
//        }
//        [HttpPut("change-password")]
//        public async Task<IActionResult> ChangeUserPassword([FromBody] ChangePassword changePassword)
//        {
//            var user = await userManager.FindByEmailAsync(changePassword.Email);
//            if (user == null)
//            {
//                return NotFound();
//            }
//            var result = await userManager.ChangePasswordAsync(user, changePassword.CurrentPassword, changePassword.NewPassword);
//            if (result.Succeeded)
//            {
//                return Ok(new { message = "Password changed successfully" });
//            }
//            return BadRequest(result.Errors);
//        }


//    }
//}
