using System.Collections.Generic;

namespace WebApiUserManagement.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public Brand Brand { get; set; }

        private readonly List<Role> _roles = new List<Role>();
        public IList<Role> Roles
        {
            get { return _roles; }
        }
    }

    public class Brand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
    }

    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}