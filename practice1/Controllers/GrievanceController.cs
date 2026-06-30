using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using practice1.Models;
using practice1.Repository;

namespace practice1.Controllers
{
    public class GrievanceController : Controller
    {
        //Pehele Controller me ek variable banana padta hai Interfacer ka taki usi ka 
        //Object ko Ham use kar sakte hai throught controller CRUD krne ke liye kuch methods ke sath
        private readonly IGrievanceRepository _repo;

        //Ye Ek Controller class ka Constructor hai jaha hm parameter pass krte hai interface with a va riable 
        //usi variable ko ham object ke sath equal kr dete hai
        public GrievanceController(IGrievanceRepository repo)
        {
            _repo = repo;
        }
        //
        //view page ko return krega Create ka form dikhayega 
        public IActionResult Create()
        {
            return View();
        }


        //iss method se data jake database me store hoga and redirect krega GrvList ko .
        
        [HttpPost]
        public IActionResult Create(Grievance grievance)
        {
            _repo.Add(grievance);

            return RedirectToAction("GrvList");
        }

        //Read Method for My controller agar koi URL nhi hoga toh by Default [HttpGET] me jayega and show hoga ek list me .
        public IActionResult GrvList()
        {
            if (HttpContext.Session.GetString("Username") == null) //sessiom
            {
                return RedirectToAction("Login", "Login"); //session
            }
            var username = HttpContext.Session.GetString("Username"); //session ke concept

            ViewBag.User = username; //session ke concept

            var grievances = _repo.GetAllGrievances();
            return View(grievances);
        }


        //Update Hoke Redirect hoke Aajayega GrivList page ko
        public IActionResult Edit(int id)
        {
            var grievance = _repo.GetById(id);

            return View(grievance);
           
        }
        [HttpPost]
        public IActionResult Edit(Grievance grievance)
        {
            _repo.update(grievance);
            TempData["Success"] = "Updated Successfully";

            return RedirectToAction("Grvlist");
        }

        public IActionResult Delete(int id)
        {
            _repo.Delete(id);
            TempData["success"] = "Deleted Successfully";
            return RedirectToAction("GrvList");
        }
}
}
