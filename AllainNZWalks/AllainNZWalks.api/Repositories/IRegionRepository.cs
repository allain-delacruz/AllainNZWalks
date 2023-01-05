using AllainNZWalks.Models.Domain;

namespace AllainNZWalks.Repositories
{
    public interface IRegionRepository
    {
       Task<IEnumerable<Region>> GetAllRegionAsync(); //Use Task<> to use async

       Task<Region> GetRegionAsync(Guid id);

       Task<Region> AddRegionAsync(Region region);

       Task<Region> DeleteRegionAsync(Guid id);

       Task<Region> UpdateRegionAsync(Guid id, Region region);
    }
}
