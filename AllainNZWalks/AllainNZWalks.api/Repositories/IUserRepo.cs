using AllainNZWalks.Models.Domain;

namespace AllainNZWalks.Repositories
{
    public interface IUserRepo
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}
