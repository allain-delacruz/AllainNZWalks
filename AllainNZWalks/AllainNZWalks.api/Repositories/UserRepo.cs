using AllainNZWalks.Data;
using AllainNZWalks.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AllainNZWalks.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly NZWalksDBContext nZWalksDBContext;

        public UserRepo(NZWalksDBContext nZWalksDBContext)
        {
            this.nZWalksDBContext = nZWalksDBContext;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user =await nZWalksDBContext.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower() && x.Password == password);

            if (user == null)
            {
                return null;
            }

            var userRoles = await nZWalksDBContext.UserRoles.Where(x => x.UserId  == user.Id).ToListAsync();

            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach (var userRole in userRoles) 
                {
                    var role = await nZWalksDBContext.Roles.FirstOrDefaultAsync(x => x.id == userRole.RoleId);
                    if (role != null) 
                    {
                        user.Roles.Add(role.RoleName);
                    }
                }
            }

            user.Password = null;
            return user;
        }
    }
}
