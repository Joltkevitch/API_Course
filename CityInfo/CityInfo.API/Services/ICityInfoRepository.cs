using CityInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        IEnumerable<City> GetCities();

        City GetCity(int cityId, bool includePointsOfInterst);

        IEnumerable<PointOfInterest> GetCityPointsOfInterest(int cityId);

        PointOfInterest GetCityPointOfInterest(int cityId, int pointOfInterestId);

        bool DoesCityExist(int cityId);

        void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);

        bool Save();

        void UpdatePointOfInterest(int cityId, PointOfInterest pointOfInterest);

        void DeletePointOfInterest(PointOfInterest pointOfInterest);
    }
}
