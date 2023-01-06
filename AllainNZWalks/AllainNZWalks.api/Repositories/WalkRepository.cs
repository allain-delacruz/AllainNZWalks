using AllainNZWalks.Data;
using AllainNZWalks.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AllainNZWalks.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDBContext nZWalksDBContext;

        public WalkRepository(NZWalksDBContext nZWalksDBContext)
        {
            this.nZWalksDBContext = nZWalksDBContext;
        }

        public async Task<Walk> AddWalkAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await nZWalksDBContext.AddAsync(walk);
            await nZWalksDBContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteWalkAsync(Guid id)
        {
            var walk = await nZWalksDBContext.Walks.FindAsync(id);

            if (walk == null)
            {
                return null;
            }

            nZWalksDBContext.Walks.Remove(walk);
            await nZWalksDBContext.SaveChangesAsync();
            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllWalkAsync()
        {
            return await 
                nZWalksDBContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetWalkAsync(Guid id)
        {
            return await nZWalksDBContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
            var existingWalk = await nZWalksDBContext.Walks.FindAsync(id);

            if (existingWalk != null)
            {
                existingWalk.Name = walk.Name;
                existingWalk.Length = walk.Length;
                existingWalk.RegionId = walk.RegionId;
                existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
                await nZWalksDBContext.SaveChangesAsync();
                return existingWalk;
            }

            return null;
        }
    }
}
