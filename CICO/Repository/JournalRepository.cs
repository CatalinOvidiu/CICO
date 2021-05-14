using CICO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CICO.Repository
{
    public class JournalRepository
    {
        //injectam container-ul ORM
        private Models.DBObjects.CICOModelsDataContext dbContext;
        public JournalRepository()
        {
            this.dbContext = new Models.DBObjects.CICOModelsDataContext();
        }
        //injectam un dbContext pentru a face repository noastra testabila
        public JournalRepository(Models.DBObjects.CICOModelsDataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<JournalModel> GetAllJournals()
        {
            List<JournalModel> journalList = new List<JournalModel>();
            foreach (Models.DBObjects.Journal dbJournal in dbContext.Journals)
            {
                journalList.Add(MapDbObjectToModel(dbJournal));
            }
            return journalList;
        }
        public JournalModel GetJournalsByID(Guid ID)
        {
            return MapDbObjectToModel(dbContext.Journals.FirstOrDefault(x => x.IdJournal == ID));
        }
        public void InsertJournal(JournalModel journalModel)
        {
            journalModel.IdJournal = Guid.NewGuid(); //generate new ID for the new record
            dbContext.Journals.InsertOnSubmit(MapModelToDbObject(journalModel));//add to ORM layer
            dbContext.SubmitChanges(); //commit to db
        }
        public void UpdateJournal(JournalModel journalModel)
        {
            //get existing record to update
            Models.DBObjects.Journal existingJournal = dbContext.Journals.FirstOrDefault(x => x.IdJournal == journalModel.IdJournal);
            if (existingJournal != null)
            {
                //map updated values with keeping the ORM object reference
                existingJournal.IdJournal = journalModel.IdJournal;
                existingJournal.IdAliment = journalModel.IdAliment;
                existingJournal.MealName = journalModel.MealName;
                existingJournal.CalloriesAmount = journalModel.CalloriesAmount;
                dbContext.SubmitChanges();//commit to db
            }
        }
        public void DeleteJournal(Guid ID)
        {
            //get existing record to delete
            Models.DBObjects.Journal journalToDelete = dbContext.Journals.FirstOrDefault(x => x.IdJournal == ID);
            if (journalToDelete != null)
            {
                dbContext.Journals.DeleteOnSubmit(journalToDelete); //mark for deletion

                dbContext.SubmitChanges(); //commit to db
            }
        }
        //map ORM model to Model object – mapper method
        private JournalModel MapDbObjectToModel(Models.DBObjects.Journal dbJournal)
        {
            JournalModel journalModel = new JournalModel();
            if (dbJournal != null)
            {
                journalModel.IdJournal = dbJournal.IdJournal;
                journalModel.IdAliment = dbJournal.IdAliment;
                journalModel.MealName = dbJournal.MealName;
                journalModel.CalloriesAmount = dbJournal.CalloriesAmount;
                return journalModel;
            }
            return null;
        }
        //map Model object to ORM model – mapper method
        private Models.DBObjects.Journal MapModelToDbObject(JournalModel journalModel)
        {
            Models.DBObjects.Journal dbJournalModel = new Models.DBObjects.Journal();
            if (journalModel != null)
            {
                dbJournalModel.IdJournal = journalModel.IdJournal;
                dbJournalModel.IdAliment = journalModel.IdAliment;
                dbJournalModel.MealName = journalModel.MealName;
                dbJournalModel.CalloriesAmount = journalModel.CalloriesAmount;
                return dbJournalModel;
            }
            return null;



        }
    }
}