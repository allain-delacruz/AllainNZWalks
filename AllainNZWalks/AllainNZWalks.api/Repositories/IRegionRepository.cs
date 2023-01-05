using AllainNZWalks.Models.Domain;

namespace AllainNZWalks.Repositories
{
    public interface IRegionRepository
    {
       Task<IEnumerable<Region>> GetAllAsync(); //Use Task<> to use async
    }
}
