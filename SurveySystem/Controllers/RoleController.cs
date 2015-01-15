using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SurveySystem.Controllers;
using SurveySystem.Models;

namespace SurveySystem.Controllers
{
    [Authorize(Roles = "ADMINISTRATOR")]
    public class RoleController : BaseController
    {
        // GET: Role
        public ActionResult Index()
        {
            var roles = Db.Roles.ToList();
            return View(roles);
        }

        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Roles/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Db.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                Db.SaveChanges();
                ViewBag.ResultMessage = "Role created successfully !";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(string RoleName)
        {
            var thisRole = Db.Roles.FirstOrDefault(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase));
            Db.Roles.Remove(thisRole);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Roles/Edit/5
        public ActionResult Edit(string roleName)
        {
            var thisRole = Db.Roles.FirstOrDefault(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));

            return View(thisRole);
        }

        // POST: /Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try
            {
                Db.Entry(role).State = System.Data.Entity.EntityState.Modified;
                Db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ManageUserRoles()
        {
            ViewBag.Roles = PrePopulateRoles();
            ViewBag.Users = PrePopulateUsers();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {
            try
            {
                ApplicationUser user =
                    Db.Users.FirstOrDefault(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase));
                IdentityRole role =
                    Db.Roles.FirstOrDefault(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase));

                IdentityUserRole userRole = new IdentityUserRole();
                userRole.RoleId = role.Id;
                userRole.UserId = user.Id;
                user.Roles.Add(userRole);

                Db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                Db.SaveChanges();

                ViewBag.ResultMessage = "Adding role was successfull!";
                ViewBag.Roles = PrePopulateRoles();
                ViewBag.Users = PrePopulateUsers();
            }
            catch (Exception)
            {
                ViewBag.ResultMessage = "Adding Role failed!";

                ViewBag.Roles = PrePopulateRoles();
                ViewBag.Users = PrePopulateUsers();

                return View("ManageUserRoles");
            }
            

            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                try
                {
                    List<string> list = new List<string>();
                    ApplicationUser user =
                    Db.Users.FirstOrDefault(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase));

                    foreach (IdentityUserRole r in user.Roles)
                    {
                        IdentityRole role = Db.Roles.Find(r.RoleId);
                        list.Add(role.Name);
                    }

                    ViewBag.RolesForThisUser = list;
                    ViewBag.Roles = PrePopulateRoles();
                    ViewBag.Users = PrePopulateUsers();
                }
                catch (Exception)
                {
                    ViewBag.ResultMessage = "Could not find roles";
                    ViewBag.Roles = PrePopulateRoles();
                    ViewBag.Users = PrePopulateUsers();

                    return View("ManageUserRoles");
                }
                
            }

            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {
            try
            {
                ApplicationUser user =
                Db.Users.FirstOrDefault(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase));
                IdentityRole role =
                        Db.Roles.FirstOrDefault(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase));

                foreach (IdentityUserRole r in user.Roles)
                {
                    if (r.RoleId == role.Id)
                    {
                        user.Roles.Remove(r);
                        Db.Entry(user).State = EntityState.Modified;
                        Db.SaveChanges();
                    }
                }
                ViewBag.Roles = PrePopulateRoles();
                ViewBag.Users = PrePopulateUsers();
            }
            catch (Exception)
            {
                ViewBag.ResultMessage = "Failed to remove role.";
                ViewBag.Roles = PrePopulateRoles();
                ViewBag.Users = PrePopulateUsers();

                return View("ManageUserRoles");
            }

            return View("ManageUserRoles");
        }

        public List<SelectListItem> PrePopulateRoles()
        {
            var list =
                Db.Roles.OrderBy(r => r.Name)
                    .ToList()
                    .Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name })
                    .ToList();
            
            return list;
        }

        public List<SelectListItem> PrePopulateUsers()
        {
            var list =
                Db.Users.OrderBy(u => u.UserName)
                    .ToList()
                    .Select(uu => new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName })
                    .ToList();

            return list;
        }
    }
}