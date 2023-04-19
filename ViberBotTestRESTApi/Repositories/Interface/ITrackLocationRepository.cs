using ViberWalkBot.Models;

namespace ViberWalkBot.Repositories.Interface
{
    public interface ITrackLocationRepository : IRepository<TrackLocation>
    {
        IQueryable<TrackLocation> GetByIMEI(string IMEI);
    }
}
