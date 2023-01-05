using AllainNZWalks.Data;
using AllainNZWalks.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AllainNZWalks.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext nZWalksDBContext;

        public RegionRepository(NZWalksDBContext nZWalksDBContext)
        {
            this.nZWalksDBContext = nZWalksDBContext;
        }

        public async Task<IEnumerable<Region>> GetAllAsync() // 'async' goes with 'await'
        {
            return await nZWalksDBContext.Regions.ToListAsync();
        }
    }
}
