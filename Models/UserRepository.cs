using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shorten_Urls.Models
{
    public class UserRepository
    {
        /// <summary>
        /// Application DB context
        /// </summary>
        protected ApplicationDbContext ApplicationDbContext { get; set; }

        /// <summary>
        /// User manager - attached to application DB context
        /// </summary>
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public List<ApplicationUser> Users { get { return UserManager.Users.ToList(); } }

        public UserRepository()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
        }

        public ApplicationUser GetById(string Id)
        {
            return UserManager.FindById(Id);
        }

        public string GetUsername(string id)
        {
            var username = string.Empty;
            try
            {
                var user = GetById(id);
                username = user.Email.Substring(0, user.Email.IndexOf('@'));

            }
            catch
            {
                //just return an empty username
            }
            return username;
        }

    }
}
