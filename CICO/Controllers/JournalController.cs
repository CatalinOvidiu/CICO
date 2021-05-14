
using CICO.Models.AlimentViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CICO.Controllers
{
    
    public class JournalController : Controller
    {
        private Repository.JournalRepository journalRepository = new Repository.JournalRepository();
        


        // GET: Journal
        [Authorize(Roles = "User, Admin")]

        public ActionResult Index()
        {
            //incarcam lista de jurnale
            List<Models.JournalModel> journals = journalRepository.GetAllJournals();

            //incarcam View-ul cu lista de modele
            return View("Index", journals);

        }

        // GET: Journal/Details/5
        [Authorize(Roles = "User, Admin")]
        public ActionResult Details(Guid id)
        {
            //incarcam modelul pe baza id-ului
            Models.JournalModel journalModel = journalRepository.GetJournalsByID(id);
            //incarcam view-ul pe baza modelului incarcat
            return View("JournalDetails", journalModel);
        }

        // GET: Journal/Create
        [Authorize(Roles = "User, Admin")]
        public ActionResult Create()
        {
            return View("CreateJournal");
        }

        // POST: Journal/Create
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // model initialization
                Models.JournalModel journalModel = new Models.JournalModel();
                //update data in model
                UpdateModel(journalModel);

                //save model
                journalRepository.InsertJournal(journalModel);
                
            //rediret to index if succesfull
                return RedirectToAction("Index");
            }
            
                
            catch
            {
                return View("CreateJournal");
            }
        }

        // GET: Journal/Edit/5
        [Authorize(Roles = "User, Admin")]
        public ActionResult Edit(Guid id)
        {
            //incarcarea datelor din db
            Models.JournalModel journalModel = journalRepository.GetJournalsByID(id);
            //incarcarea viewlui prin trimitere model incarcat cu date
            return View("EditJournal", journalModel);

        }

        // POST: Journal/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                //instantiem modelul
                Models.JournalModel journalModel = new Models.JournalModel();
                //incarcam datele in model
                UpdateModel(journalModel);
                //apelam resursa care salveaza datele
                journalRepository.UpdateJournal(journalModel);
                //redirectare catre index in caz de succes
                return RedirectToAction("Index");
            }
            catch
            {
                return View("EditJournal");
            }
        }

        // GET: Journal/Delete/5
        [Authorize(Roles = "User, Admin")]
        public ActionResult Delete(Guid id)
        {
            //incarcam datele in model din db
            Models.JournalModel journalModel = journalRepository.GetJournalsByID(id);
            //incarcam view-ul cu modelul atasat
            return View("DeleteJournal", journalModel);

        }

        // POST: Journal/Delete/5
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                //apelam repository care sterge datele
                journalRepository.DeleteJournal(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteJournal");
            }
        }
    }
}
