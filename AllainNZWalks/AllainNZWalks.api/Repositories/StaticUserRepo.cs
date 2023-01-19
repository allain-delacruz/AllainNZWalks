using AllainNZWalks.Models.Domain;

namespace AllainNZWalks.Repositories
{
    public class StaticUserRepo : IUserRepo
    {
        private List<User> Users = new List<User>()
        {
            //new User()
            //{
            //    FirstName = "Read Only",
            //    LastName = "User",
            //    EmailAddress = "readonly@user.com",
            //    Id = new Guid(),
            //    UserName = "readonly@user.com",
            //    Password = "ReadOnly@User",
            //    Roles = new List<string> {"reader"}
            //},
            //new User()
            //{
            //    FirstName = "Read Write",
            //    LastName = "User",
            //    EmailAddress = "readwrite@user.com",
            //    Id = new Guid(),
            //    UserName = "readwrite@user.com",
            //    Password = "ReadWrite@User",
            //    Roles = new List<string> {"reader", "writer"}
            //}
        };

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = Users.Find(x => x.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase) && x.Password == password);

            return user; 
        }
    }
}
