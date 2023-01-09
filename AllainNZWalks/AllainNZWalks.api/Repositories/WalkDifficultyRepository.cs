using AllainNZWalks.Data;
using AllainNZWalks.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AllainNZWalks.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDBContext nZWalksDBContext;

        public WalkDifficultyRepository(NZWalksDBContext nZWalksDBContext)
        {
            this.nZWalksDBContext = nZWalksDBContext;
        }

        public async Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await nZWalksDBContext.AddAsync(walkDifficulty);
            await nZWalksDBContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteWalkDifficultyAsync(Guid id)
        {
            var walkDifficulty = await nZWalksDBContext.WalkDifficulty.FindAsync(id);

            if (walkDifficulty == null)
            {
                return null;
            }

            nZWalksDBContext.WalkDifficulty.Remove(walkDifficulty);
            await nZWalksDBContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficultyAsync()
        {
            return await nZWalksDBContext.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetWalkDifficultyAsync(Guid id)
        {
            return await nZWalksDBContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingWalkD = await nZWalksDBContext.WalkDifficulty.FindAsync(id);

            if (existingWalkD != null)
            {
                existingWalkD.Code = walkDifficulty.Code;
                await nZWalksDBContext.SaveChangesAsync();
                return existingWalkD;
            }

            return null;
        }
    }
}
