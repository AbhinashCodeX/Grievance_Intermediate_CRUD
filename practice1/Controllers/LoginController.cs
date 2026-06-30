using Microsoft.AspNetCore.Mvc;
using practice1.Models;
using practice1.Repository;
namespace practice1.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUserRepository _repo;

        public LoginController(IUserRepository repo)
        {
            _repo = repo;
        }
        //Jab user (/Login/Login) Controller bolega => (Login.cshtml dikhao.)
        public IActionResult Login()
        {
            return View();
        }
        //Jav user login form bharta hai ye method logic dekhega
        [HttpPost]
        public IActionResult Login(User user) //user ek object he jo Model Binding ka concept use krke Object DB ke sath map krke leke aata hai
        {
            var result = _repo.Login(user);
            if(result!= null)
            {
                HttpContext.Session.SetInt32("UserId", result.UserId); //session concept ka username

                HttpContext.Session.SetString("Username", result.Username); //session concept ka password

                return RedirectToAction("GrvList", "Grievance");
            }
            else
            {
                ViewBag.Message = "Invalid Username or Password";

                return View();
            }


        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
