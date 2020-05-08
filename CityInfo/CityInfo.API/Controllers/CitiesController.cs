
using AutoMapper;
using CityInfo.API.CityInfo.API;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{

    // Controller base has basci functionalities a controller needs, such as the user
    [ApiController] // adds features that make making an API a easier experience
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            var cities = _cityInfoRepository.GetCities();
            var results = _mapper.Map<IEnumerable<CityWithOutPointsOfInterest>>(cities);

            return Ok(results);
        }

        [HttpGet("{id}")] // the brackets indicates that's an outside variable from the URI
        public IActionResult GetCity(int id, bool includePointsOfInterest = false)
        {
            var city = _cityInfoRepository.GetCity(id, includePointsOfInterest);
            if (city == null)
            {
                return NotFound();
            }

            if (includePointsOfInterest)
            {
                var result = _mapper.Map<CityDto>(city);

                return Ok(result);
            }
            else
            {
                var result = _mapper.Map<CityWithOutPointsOfInterest>(city);

                return Ok(result);
            }
        }
    }
}
