using Microsoft.EntityFrameworkCore;
using ViberWalkBot.Context;
using ViberWalkBot.Models;
using ViberWalkBot.Repositories.Interface;

namespace ViberWalkBot.Repositories
{
    public class TrackLocationRepository : ITrackLocationRepository
    {

        private readonly AppDbContext _appDbContext;
        private readonly ILogger<TrackLocationRepository> _logger;

        public TrackLocationRepository(AppDbContext appDbContext, ILogger<TrackLocationRepository> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public Task<TrackLocation> EditAsync(TrackLocation entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TrackLocation> GetAll()
        {
            try
            {
                return _appDbContext.TrackLocation;
            }
            catch (Exception ex)
            {

                return LogError<IQueryable<TrackLocation>>(ex);
            }

        }

        public async Task<TrackLocation> GetAsync(int id)
        {
            try
            {
                var model = await GetAll().SingleOrDefaultAsync(tl => tl.Id == id);
                if (model == null) return new();
                return model;
            }
            catch (Exception ex)
            {
                return LogError<TrackLocation>(ex);
            }

        }

        public IQueryable<TrackLocation> GetByIMEI(string IMEI)
        {
            try
            {
                IQueryable<TrackLocation> model = GetAll().Where(tl => tl.IMEI == IMEI);
                if (model == null) return default(IQueryable<TrackLocation>);
                return model;
            }
            catch (Exception ex)
            {
                return LogError<IQueryable<TrackLocation>>(ex);
            }
        }

        public Task<TrackLocation> UpdateAsync(TrackLocation entity)
        {
            throw new NotImplementedException();
        }

        private T LogError<T>(Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.Message);
            return default(T);
        }
    }
}
