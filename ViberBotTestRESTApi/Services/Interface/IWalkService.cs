using ViberWalkBot.Models;

namespace ViberWalkBot.Services.Interface
{
    public interface IWalkService
    {
        IQueryable<Walk> GetAllWalk();
        IQueryable<Walk> GetAllWalkByIMEI(string IMEI);
        IEnumerable<Walk> GetTop10(string IMEI, int numberElements);
        Task<IEnumerable<Walk>> AddWalksAsync(IEnumerable<Walk> walk);
    }
}
