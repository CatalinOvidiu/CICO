using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CICO.Controllers
{
    public class UserController : Controller
    {
        private Repository.UserRepository userRepository = new Repository.UserRepository();
        // GET: User
        [Authorize(Roles = "User, Admin")]
        public ActionResult Index()
        {
            //incarcam lista de useri
            List<Models.UserModel> users = userRepository.GetAllUsers();
            //incarcam View-ul cu lista de modele
            return View("Index", users);

        }

        // GET: User/Details/5
        [Authorize(Roles = "User, Admin")]
        public ActionResult Details(Guid id)
        {
            //incarcam modelul pe baza id-ului
            Models.UserModel userModel = userRepository.GetUsersByID(id);
            //incarcam view-ul pe baza modelului incarcat
            return View("UserDetails", userModel);

        }

        // GET: User/Create
        [Authorize(Roles = "User, Admin")]
        public ActionResult Create()
        {
            return View("CreateUser");
        }

        // POST: User/Create
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Models.UserModel userModel = new Models.UserModel();
                //update data in model
                UpdateModel(userModel);

                //save model
                userRepository.InsertUser(userModel);

                //rediret to index if succesfull
                return RedirectToAction("Index");
            }

            catch
            {
                return View("CreateUser");
            }
        }

        // GET: User/Edit/5
        [Authorize(Roles = "User, Admin")]
        public ActionResult Edit(Guid id)
        {
            //incarcarea datelor din db
            Models.UserModel userModel = userRepository.GetUsersByID(id);
            //incarcarea viewlui prin trimitere model incarcat cu date
            return View("EditUser", userModel);
        }

        // POST: User/Edit/5
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                //initializam modelul
                Models.UserModel userModel = userRepository.GetUsersByID(id);
                
                //incarcam datele in model
                UpdateModel(userModel);
                
                //apelam resursa care salveaza datele
                userRepository.UpdateUser(userModel);

                //redirectare catre index in caz de succes
                return RedirectToAction("Index");


            }
            catch
            {
                return View("EditUser");
            }
        }

        // GET: User/Delete/5
        [Authorize(Roles = "User, Admin")]
        public ActionResult Delete(Guid id)
        {
            //incarcam datele in model din db
            Models.UserModel userModel = userRepository.GetUsersByID(id);

            //incarcam view-ul cu modelul atasat
            return View("DeleteUser", userModel);
        }

        // POST: User/Delete/5
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                //apelam repository care sterge datele
                userRepository.DeleteUser(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteUser");
            }
        }
    }
}
