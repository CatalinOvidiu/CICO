using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CICO.Controllers
{
    public class AlimentController : Controller
    {
        private Repository.AlimentRepository alimentRepository = new Repository.AlimentRepository();
        // GET: Aliment
        [Authorize(Roles = "User, Admin")]

        public ActionResult Index()
        {
            //incarcam lista de anunturi
            List<Models.AlimentModel> aliments = alimentRepository.GetAllAliments();
            //incarcam View-ul cu lista de modele
            return View("Index", aliments);

        }

        // GET: Aliment/Details/5
        [Authorize(Roles = "User, Admin")]
        public ActionResult Details(Guid id)
        {
            Models.AlimentModel alimentModel = alimentRepository.GetAlimentsByID(id);
            //incarcam view-ul pe baza modelului incarcat
            return View("AlimentDetails", alimentModel);
        }

        // GET: Aliment/Create
        [Authorize(Roles = "User, Admin")]
        public ActionResult Create()
        {
            return View("CreateAliment");
        }

        // POST: Aliment/Create
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Models.AlimentModel alimentModel = new Models.AlimentModel();
                //update data in model
                UpdateModel(alimentModel);

                //save model
                alimentRepository.InsertAliment(alimentModel);

                //rediret to index if succesfull

                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateAliment");
            }
        }

        // GET: Aliment/Edit/5
        [Authorize(Roles = "User, Admin")]
        public ActionResult Edit(Guid id)
        {
            //incarcarea datelor din db
            Models.AlimentModel alimentModel = alimentRepository.GetAlimentsByID(id);
            //incarcarea viewlui prin trimitere model incarcat cu date
            return View("EditAliment", alimentModel);
        }

        // POST: Aliment/Edit/5
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                Models.AlimentModel alimentModel = new Models.AlimentModel();
                //incarcam datele in model
                UpdateModel(alimentModel);
                //apelam resursa care salveaza datele
                alimentRepository.UpdateAliment(alimentModel);
                //redirectare catre index in caz de succes
                return RedirectToAction("Index");

            }
            catch
            {
                return View("EditAliment");
            }
        }

        // GET: Aliment/Delete/5
        [Authorize(Roles = "User, Admin")]
        public ActionResult Delete(Guid id)
        {
            //incarcam datele in model din db
            Models.AlimentModel alimentModel = alimentRepository.GetAlimentsByID(id);
            //incarcam view-ul cu modelul atasat
            return View("DeleteAliment", alimentModel);
        }

        // POST: Aliment/Delete/5
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                //apelam repository care sterge datele
                alimentRepository.DeleteAliment(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteAliment");
            }
        }
    }
}
