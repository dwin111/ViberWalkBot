using ViberWalkBot.Models;
using ViberWalkBot.Repositories.Interface;
using ViberWalkBot.Services.Interface;

namespace ViberWalkBot.Services
{
    public class WalkService : IWalkService
    {

        private readonly IWalkRepository _walkRepository;

        public WalkService(IWalkRepository walkRepository)
        {
            _walkRepository = walkRepository;
        }

        public IQueryable<Walk> GetAllWalk()
        {
            try
            {
                var walk = _walkRepository.GetAll();
                return walk;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<IEnumerable<Walk>> AddWalksAsync(IEnumerable<Walk> walk)
        {
            try
            {
                await _walkRepository.CreateWalksAsync(walk);
                return walk;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IQueryable<Walk> GetAllWalkByIMEI(string IMEI)
        {
            try
            {
                var walks = GetAllWalk().Where(w => w.IMEI == IMEI);
                return walks;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<Walk> GetTop10(string IMEI, int numberElements)
        {
            try
            {
                var top10walk = GetAllWalkByIMEI(IMEI).Where(wl => wl.Distance != 0).OrderByDescending(wl => wl.Distance);
                if (top10walk.Count() > numberElements)
                {
                    return top10walk.Take(numberElements);
                }
                else { return top10walk; }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
