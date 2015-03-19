using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoleSample.Models;
using System.Configuration;
using System.Data.SqlClient;
using RoleSample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RoleSample.Controllers
{
    public class HomeController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        //取得組態檔案中對應的連線字串
        public static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        SqlCommand cmd = new SqlCommand();

        List<string> test = new List<string>();

        //[Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<MenuItemModel> menuItem = new List<MenuItemModel>();

            var name = HttpContext.User.Identity.Name;
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            //取得 User ID / Role
            string currentUserId = User.Identity.GetUserId();
            string roles = string.Empty;
            if (!string.IsNullOrEmpty(currentUserId))
            {
                roles = um.GetRoles(currentUserId).FirstOrDefault();
            }
            else
            {
                roles = "";
            }


            //依 Role判斷 SQL cmd
            SqlCommand cmd = new SqlCommand();
            if (roles.Equals("Admin"))
                cmd.CommandText = "select * from AspNetMenu ";
            else
                cmd.CommandText = "select * from AspNetMenu where RoleName= '" + roles + "'";

            //成功登入後，顯示MENU內容
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cmd.Connection = cn;
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    try
                    {
                        while (dr.Read())
                        {
                            MenuItemModel item = new MenuItemModel();

                            item.Id = Convert.ToInt32(dr["Id"].ToString());
                            item.Parent = Convert.ToInt32(dr["Parent"].ToString());
                            item.Order = Convert.ToInt32(dr["Order"].ToString());
                            item.Name = dr["Name"].ToString();
                            item.RoleName = dr["RoleName"].ToString();
                            item.Controller = dr["Controller"].ToString();
                            item.Action = dr["Action"].ToString();

                            menuItem.Add(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString());
                    }
                }
            }
            //return RedirectToAction("Menu/Index");
            return View(menuItem.AsEnumerable());
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        [Authorize(Roles = "Visitor")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}