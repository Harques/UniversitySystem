using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{

    public class UserMasterRepository : IDisposable
    {
        private UniversityEntities context;
        public UserMasterRepository()
        {
            context = new UniversityEntities();
        }
        public USER ValidateUser(string email, string password)
        {
            return context.USERS.FirstOrDefault(user =>
            user.email.Equals(email, StringComparison.OrdinalIgnoreCase)
            && user.password == password);
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}