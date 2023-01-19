using AllainNZWalks.Models.Domain;

namespace AllainNZWalks.Repositories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
