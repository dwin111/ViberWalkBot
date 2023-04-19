using ViberWalkBot.Context;
using ViberWalkBot.Models;
using ViberWalkBot.Repositories.Interface;

namespace ViberWalkBot.Repositories
{
    public class WalkRepository : IWalkRepository
    {

        private readonly AppDbContext _appDbContext;
        private readonly ILogger<WalkRepository> _logger;

        public WalkRepository(AppDbContext appDbContext, ILogger<WalkRepository> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public Task<Walk> CreateWalkAsync(Walk walk)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Walk>> CreateWalksAsync(IEnumerable<Walk> walk)
        {
            try
            {
                await _appDbContext.Walks.AddRangeAsync(walk);
                await _appDbContext.SaveChangesAsync();
                return walk;
            }
            catch (Exception ex)
            {

                return LogError<IEnumerable<Walk>>(ex);
            }
        }

        public Task<Walk> EditAsync(Walk entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Walk> GetAll()
        {
            try
            {
                return _appDbContext.Walks;
            }
            catch (Exception ex)
            {

                return LogError<IQueryable<Walk>>(ex);
            }
        }

        public Task<Walk> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Walk> UpdateAsync(Walk entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Walk>> UpdatesAsync(IEnumerable<Walk> entity)
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<Walk>> UpdatesAsync(IEnumerable<Walk> entity)
        //{
        //    try
        //    {
        //        await _appDbContext.Walks. ;
        //        await _appDbContext.SaveChangesAsync();
        //        return walk;
        //    }
        //    catch (Exception ex)
        //    {

        //        return LogError<IEnumerable<Walk>>(ex);
        //    }
        //}

        private T LogError<T>(Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.Message);
            return default(T);
        }
    }
}
