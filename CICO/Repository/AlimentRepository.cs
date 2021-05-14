using CICO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CICO.Repository
{
    public class AlimentRepository
    {
        //injectam container-ul ORM
        private Models.DBObjects.CICOModelsDataContext dbContext;
        public AlimentRepository()
        {
            this.dbContext = new Models.DBObjects.CICOModelsDataContext();
        }
        //injectam un dbContext pentru a face repository noastra testabila
        public AlimentRepository(Models.DBObjects.CICOModelsDataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<AlimentModel> GetAllAliments()
        {
            List<AlimentModel> alimentList = new List<AlimentModel>();
            foreach (Models.DBObjects.Aliment dbAliement in dbContext.Aliments)
            {
                alimentList.Add(MapDbObjectToModel(dbAliement));
            }
            return alimentList;
        }
        public AlimentModel GetAlimentsByID(Guid ID)
        {
            return MapDbObjectToModel(dbContext.Aliments.FirstOrDefault(x => x.IdAliment == ID));
        }
        public void InsertAliment(AlimentModel alimentModel)
        {
            alimentModel.IdAliment = Guid.NewGuid(); //generate new ID for the new record
            dbContext.Aliments.InsertOnSubmit(MapModelToDbObject(alimentModel));//add to ORM layer
            dbContext.SubmitChanges(); //commit to db
        }
        public void UpdateAliment(AlimentModel alimentModel)
        {
            //get existing record to update
            Models.DBObjects.Aliment existingAliment = dbContext.Aliments.FirstOrDefault(x => x.IdAliment == alimentModel.IdAliment);
            if (existingAliment != null)
            {
                //map updated values with keeping the ORM object reference
                existingAliment.IdAliment = alimentModel.IdAliment;
                existingAliment.Name = alimentModel.Name;
                dbContext.SubmitChanges();//commit to db
            }
        }
        public void DeleteAliment(Guid ID)
        {
            //get existing record to delete
            Models.DBObjects.Aliment alimentToDelete = dbContext.Aliments.FirstOrDefault(x => x.IdAliment == ID);
            if (alimentToDelete != null)
            {
                dbContext.Aliments.DeleteOnSubmit(alimentToDelete); //mark for deletion

                dbContext.SubmitChanges(); //commit to db
            }
        }
        //map ORM model to Model object – mapper method
        private AlimentModel MapDbObjectToModel(Models.DBObjects.Aliment dbAliment)
        {
            AlimentModel alimentModel = new AlimentModel();
            if (dbAliment != null)
            {
                alimentModel.IdAliment = dbAliment.IdAliment;
                alimentModel.Name = dbAliment.Name;
                return alimentModel;
            }
            return null;
        }
        //map Model object to ORM model – mapper method
        private Models.DBObjects.Aliment MapModelToDbObject(AlimentModel alimentModel)
        {
            Models.DBObjects.Aliment dbAlimentModel = new Models.DBObjects.Aliment();
            if (alimentModel != null)
            {
                dbAlimentModel.IdAliment = alimentModel.IdAliment;
                dbAlimentModel.Name = alimentModel.Name;
                return dbAlimentModel;
            }
            return null;

        }
    }
}