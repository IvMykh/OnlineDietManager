using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlineDietManager.Domain.UsersManagement
{
    public class AppRole
        : IdentityRole
    {
        public enum RoleType
        {
            Admin,
            User
        }

        public static string RoleTypeToString(RoleType roleType)
        {
            switch (roleType)
            {
                case RoleType.Admin:    return "Admin";
                case RoleType.User:     return "User";

                default: throw new NotImplementedException(
                    string.Format("Role '{0}' is not implemented", roleType.ToString()));
            }
        }

        public AppRole()
            : base()
        {
        }

        public AppRole(string name)
            : base(name)
        {
        }
    }
}
