using ViberWalkBot.Models;

namespace ViberWalkBot.Services.Interface
{
    public interface ITrackLocationService
    {
        public IQueryable<TrackLocation> GetDataTrackByIMEI(string IMEI);
        public Task<List<Walk>> GenerationWalks(string IMEI);

    }
}
