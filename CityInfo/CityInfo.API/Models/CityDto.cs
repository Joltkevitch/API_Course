using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int NumberOfPointsOfInterest // the consumer of the api wont have a say in this, is somethign determine internaly
        {
            get
            {
                return PointsOfInterest.Count(); // This is calculated data
            }
        }

        public ICollection<PointOfInterestDto> PointsOfInterest { get; set; } = new List<PointOfInterestDto>();
    }
}
