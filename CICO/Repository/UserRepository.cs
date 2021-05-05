using CICO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CICO.Repository
{
    public class UserRepository
    {
        //injectam container-ul ORM
        private Models.DBObjects.CICOModelsDataContext dbContext;
        public UserRepository()
        {
            this.dbContext = new Models.DBObjects.CICOModelsDataContext();
        }
        //injectam un dbContext pentru a face repository noastra testabila
        public UserRepository(Models.DBObjects.CICOModelsDataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<UserModel> GetAllUsers()
        {
            List<UserModel> userList = new List<UserModel>();
            foreach (Models.DBObjects._User dbUser in dbContext._Users)
            {
                userList.Add(MapDbObjectToModel(dbUser));
            }
            return userList;
        }
        public UserModel GetUsersByID(Guid ID)
        {
            return MapDbObjectToModel(dbContext._Users.FirstOrDefault(x => x.IdUser == ID));
        }
        public void InsertUser(UserModel userModel)
        {
            userModel.IdUser = Guid.NewGuid(); //generate new ID for the new record
            dbContext._Users.InsertOnSubmit(MapModelToDbObject(userModel));//add to ORM layer
            dbContext.SubmitChanges(); //commit to db
        }
        public void UpdateUser(UserModel userModel)
        {
            //get existing record to update
            Models.DBObjects._User existingUser = dbContext._Users.FirstOrDefault(x => x.IdUser == userModel.IdUser);
            if (existingUser != null)
            {
                //map updated values with keeping the ORM object reference
                existingUser.IdUser = userModel.IdUser;
                existingUser.Name = userModel.Name;
                existingUser.Weight = userModel.Weight;
                existingUser.Height = userModel.Height;
                existingUser.Age = userModel.Age;
                existingUser.CalloriesRequirements = userModel.CalloeieRequirements;
                dbContext.SubmitChanges();//commit to db
            }
        }
        public void DeleteUser(Guid ID)
        {
            //get existing record to delete
            Models.DBObjects._User userToDelete = dbContext._Users.FirstOrDefault(x => x.IdUser == ID);
            if (userToDelete != null)
            {
                dbContext._Users.DeleteOnSubmit(userToDelete); //mark for deletion

                dbContext.SubmitChanges(); //commit to db
            }
        }
        //map ORM model to Model object – mapper method
        private UserModel MapDbObjectToModel(Models.DBObjects._User dbUser)
        {
            UserModel userModel = new UserModel();
            if (dbUser != null)
            {
                userModel.IdUser = dbUser.IdUser;
                userModel.Name = dbUser.Name;
                userModel.Weight = dbUser.Weight;
                userModel.Height = dbUser.Height;
                userModel.Age = dbUser.Age;
                userModel.CalloeieRequirements = dbUser.CalloriesRequirements;
                return userModel;
            }
            return null;
        }
        //map Model object to ORM model – mapper method
        private Models.DBObjects._User MapModelToDbObject(UserModel userModel)
        {
            Models.DBObjects._User dbUserModel = new Models.DBObjects._User();
            if (userModel != null)
            {
                dbUserModel.IdUser = userModel.IdUser;
                dbUserModel.Name = userModel.Name;
                dbUserModel.Weight = userModel.Weight;
                dbUserModel.Height = userModel.Height;
                dbUserModel.Age = userModel.Age;
                dbUserModel.CalloriesRequirements = userModel.CalloeieRequirements;
                return dbUserModel;
            }
            return null;

        }
    }
}