using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestTaskDocuments.Data;
using TestTaskDocuments.Models;

namespace TestTaskDocuments.Controllers
{
    public class ListOfUsersController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        public ListOfUsersController(UserManager<ApplicationUser> manager)
        {
            _userManager = manager;
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.ToList();

            return View(users);
        }

        public async Task<IActionResult> GetInfoAboutUser(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            var listRoles = await _userManager.GetRolesAsync(user);
            ViewBag.Role = listRoles.FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        public async Task<RedirectResult> ActionWithUser(string roleFromDropDownList, string action, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var listRoles = await _userManager.GetRolesAsync(user);
            var userRole = listRoles.FirstOrDefault();
            if (action == "save")
            {
                if (roleFromDropDownList != null)
                {
                    await _userManager.RemoveFromRoleAsync(user, userRole);
                    await _userManager.AddToRoleAsync(user, roleFromDropDownList);
                }
            }
            else if (action == "deleteUser")
            {
               await _userManager.DeleteAsync(user);
            }
            return Redirect("GetAllUsers");
        }

    }
}