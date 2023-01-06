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

        public async Task<Region> AddRegionAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nZWalksDBContext.AddAsync(region);
            await nZWalksDBContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteRegionAsync(Guid id)
        {
            var region = await nZWalksDBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (region == null)
            {
                return null;
            }

            //Delete the Region
            nZWalksDBContext.Regions.Remove(region);
            await nZWalksDBContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllRegionAsync() // 'async' goes with 'await'
        {
            return await 
                nZWalksDBContext.Regions
                .Include(x => x.Walks)
                .ToListAsync();
        }

        public async Task<Region> GetRegionAsync(Guid id)
        {
            return await nZWalksDBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateRegionAsync(Guid id, Region region)
        {
           var existingRegion = await nZWalksDBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

           if (region == null)
           {
               return null;
           }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            await nZWalksDBContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}
