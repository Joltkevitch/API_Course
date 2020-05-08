using CityInfo.API.Contexts;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {

        private readonly CityInfoContext _context;
        public CityInfoRepository(CityInfoContext cityInfocontext)
        {
            _context = cityInfocontext ?? throw new ArgumentNullException(nameof(cityInfocontext)); ;
        }
        public IEnumerable<City> GetCities()
        {
            return _context.Cities.OrderBy(x => x.Name).ToList();
        }

        public City GetCity(int cityId, bool includePointsOfInterst)
        {
            if (includePointsOfInterst)
            {
                return _context.Cities.Include(x => x.PointsOfInterest).Where(y => y.Id == cityId).FirstOrDefault();
            }

            return _context.Cities.Where(x => x.Id == cityId).FirstOrDefault();
        }

        public PointOfInterest GetCityPointOfInterest(int cityId, int pointOfInterestId)
        {
            return _context.PointsOfInterest.Where(x => x.Id == pointOfInterestId && x.CityId == cityId).FirstOrDefault();
        }

        public IEnumerable<PointOfInterest> GetCityPointsOfInterest(int cityId)
        {
            return _context.PointsOfInterest.Where(x => x.CityId == cityId).ToList();
        }

        public bool DoesCityExist(int cityId)
        {
            return _context.Cities.Any<City>(x => int.Parse(x.Id.ToString()) == int.Parse(cityId.ToString()));
        }

        public void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterestToAdd)
        {
            var city = GetCity(cityId, false);
            city.PointsOfInterest.Add(pointOfInterestToAdd);
        }

        public void UpdatePointOfInterest(int cityId, PointOfInterest pointOfInterest) { }

        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.PointsOfInterest.Remove(pointOfInterest);
        }


        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
