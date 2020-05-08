using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<PointOfInterest, PointOfInterestDto>().ReverseMap();
            CreateMap<PointOfInterest, PointOfInterestForCreationDto>().ReverseMap();
            CreateMap<PointOfInterestForUpdate, PointOfInterest>().ReverseMap();
        }
    }
}
