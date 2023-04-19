using GeoCoordinatePortable;
using Microsoft.EntityFrameworkCore;
using ViberWalkBot.Models;
using ViberWalkBot.Repositories.Interface;
using ViberWalkBot.Services.Interface;

namespace ViberWalkBot.Services
{
    public class TrackLocationService : ITrackLocationService
    {
        private readonly ITrackLocationRepository _trackLocationRepository;
        private readonly IWalkService _walkService;

        public TrackLocationService(ITrackLocationRepository trackLocationRepository, IWalkService walkService)
        {
            _trackLocationRepository = trackLocationRepository;
            _walkService = walkService;
        }

        public IQueryable<TrackLocation> GetDataTrackByIMEI(string IMEI)
        {
            try
            {
                var models = _trackLocationRepository.GetByIMEI(IMEI).OrderBy(tl => tl.Date_Track);
                return models;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<Walk>> GenerationWalks(string IMEI)
        {
            try
            {
                List<TrackLocation> model = GetDataTrackByIMEI(IMEI).ToList();

                double interwallOneWallk = 0;
                int idStartWalk = 0;
                List<Walk> Walks = new();

                double Latitude0 = (double)model[0].Latitude;
                double Longitude0 = (double)model[0].Longitude;
                double AlldistanceOneWalk = 0;

                double NumberWalk = 1;

                for (int i = 0; i < model.Count() - 1; i++)
                {
                    TimeSpan interval = model[i + 1].Date_Track - model[i].Date_Track;
                    if (interval.TotalMinutes <= 30 && (Latitude0 != (double)model[i].Latitude || Longitude0 != (double)model[i].Longitude))
                    {
                        GeoCoordinate pin1 = new GeoCoordinate(Latitude0, Longitude0);
                        GeoCoordinate pin2 = new GeoCoordinate((double)model[i].Latitude, (double)model[i].Longitude);

                        double distanceBetween = pin1.GetDistanceTo(pin2);
                        AlldistanceOneWalk += distanceBetween;

                        Latitude0 = (double)model[i].Latitude;
                        Longitude0 = (double)model[i].Longitude;
                    }
                    if (interval.TotalMinutes > 30)
                    {
                        interval = model[i].Date_Track - model[idStartWalk].Date_Track;
                        interwallOneWallk = interval.TotalMinutes;
                        if (interwallOneWallk != 0)
                        {
                            var walk = new Walk
                            {
                                IMEI = IMEI,
                                Name = "Прогулянка: " + NumberWalk,
                                Time = interwallOneWallk,
                                Distance = AlldistanceOneWalk
                            };
                            if (!await _walkService.GetAllWalk().AnyAsync(w => w.IMEI == walk.IMEI && w.Name == walk.Name))
                            {
                                Walks.Add(walk);
                            }
                            interwallOneWallk = 0;
                            idStartWalk = i + 1;
                            AlldistanceOneWalk = 0;

                            NumberWalk++;

                            Latitude0 = (double)model[i + 1].Latitude;
                            Longitude0 = (double)model[i + 1].Longitude;
                        }
                        else
                        {
                            interwallOneWallk = 0;
                            idStartWalk = i + 1;
                        }
                    }

                }
                if (Walks != new List<Walk>())
                {
                    await _walkService.AddWalksAsync(Walks);
                }
                return await _walkService.GetAllWalkByIMEI(IMEI).ToListAsync();

            }
            catch (Exception ex)
            {

                throw;
            }
        }



    }
}
