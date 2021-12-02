using System.Collections.Generic;

namespace JwtAuth.Models
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel()
            {
                Username = "@Jason_admin",
                EmailAddress = "jason.admin@email.com",
                Password = "P@ss_w0rd",
                GivenName = "Jason",
                Surname = "Bryant",
                Role = "Admin"
            },
            new UserModel()
            {
                Username = "@Gypsi",
                EmailAddress = "gypsi.seller@email.com",
                Password = "P@ss_w0rd",
                GivenName = "Gypsi",
                Surname = "Mugane",
                Role = "Seller"
            }
        };
    }
}
