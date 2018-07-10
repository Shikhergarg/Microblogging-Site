using DataAccessLayer.Entity;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class UserRepository : RepositoryBase
    {
        public List<UserDTO> GetAllUsers()
        {
            List<UserDTO> retVal = null;

            try
            {
                using(GlitterDBEntities dbContext = new GlitterDBEntities())
                {
                    var users = dbContext.Users.ToList();

                    retVal = users != null && users.Count > 0 ? new List<UserDTO>() : null;

                    //users.ForEach(x =>
                    //{
                    //    retVal.Add(new UserDTO()
                    //    {
                    //        EmailId = x.EmailId,
                    //        FirstName = x.FirstName,
                    //        LastName = x.LastName,
                    //        ContactNumber = x.ContactNumber,
                    //        Country = x.Country,
                    //        Password = x.Password
                    //    });    
                    //});
                    retVal= this.Mapper.Map<List<UserDTO>>(users);

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return retVal;
        }
		public bool ValidateUser(string emailId, string password)
		{
			bool isValid = false;

			try
			{
				using (GlitterDBEntities dbContext = new GlitterDBEntities())
				{
					var user = dbContext.Users.Single(x => x.EmailId.ToLower().Equals(emailId.ToLower()) && x.Password.Equals(password));
					isValid = user != null;
				}
			}
			catch
			{
				isValid = false;
			}

			return isValid;
		}
		public void RegisterUser(UserDTO newUser)
		{
			try
			{
				using (GlitterDBEntities dbContext = new GlitterDBEntities())
				{
					//dbContext.Users.Add(this.Mapper.Map<User>(newUser));


					dbContext.Users.Add(this.Mapper.Map<User>(newUser));
					dbContext.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public UserDTO GetUserByEmailId(string email)
		{
			UserDTO retVal = new UserDTO();
			try
			{
				using (GlitterDBEntities dbContext = new GlitterDBEntities())
				{
					var user = dbContext.Users.Where(x => x.EmailId == email).FirstOrDefault();
					//retVal = users != null && users.Count > 0 ? new List<UserDTO>() : null;

					retVal = this.Mapper.Map<UserDTO>(user);

				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
			return retVal;
		}
		public UserDTO Login(string email, string password)
		{
			UserDTO retVal = null;
			try
			{
				using (GlitterDBEntities dbContext = new GlitterDBEntities())
				{
					var user = dbContext.Users.Where(u => u.EmailId == email && u.Password == password).FirstOrDefault();

					retVal = this.Mapper.Map<UserDTO>(user);

				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
			return retVal;
		}



	}
}


