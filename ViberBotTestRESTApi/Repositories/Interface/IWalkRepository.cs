using ViberWalkBot.Models;

namespace ViberWalkBot.Repositories.Interface
{
    public interface IWalkRepository : IRepository<Walk>
    {
        Task<Walk> CreateWalkAsync(Walk walk);
        Task<IEnumerable<Walk>> CreateWalksAsync(IEnumerable<Walk> walk);
        Task<IEnumerable<Walk>> UpdatesAsync(IEnumerable<Walk> entity);
    }
}
