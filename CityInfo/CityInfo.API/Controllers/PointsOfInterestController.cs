using AutoMapper;
using CityInfo.API.CityInfo.API;
using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointsOfInterestController : ControllerBase
    {

        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;


        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService mailService, ICityInfoRepository cityInforRepository, IMapper mapper)
        {
            _logger = logger;
            _mailService = mailService;
            _cityInfoRepository = cityInforRepository;
            _mapper = mapper;
        }

        [HttpGet()]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            try
            {

                if (!_cityInfoRepository.DoesCityExist(cityId))
                {
                    _logger.LogInformation($"The city with the Id {cityId} can't be found.");
                    return NotFound();
                }

                var pointsOfInterest = _cityInfoRepository.GetCityPointsOfInterest(cityId);

                return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterest));
            }
            catch (Exception error)
            {
                _logger.LogCritical($"The city with the Id {cityId} can't be found.", error);
                return StatusCode(500, "A problem happened");
            }

        }

        [HttpGet("{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {

            if (!_cityInfoRepository.DoesCityExist(cityId))
            {
                return NotFound();
            }
            var cityToReturn = _cityInfoRepository.GetCity(cityId, true);

            var pointOfInterst = _cityInfoRepository.GetCityPointOfInterest(cityId, id);

            if (pointOfInterst == null)
            {
                return NotFound();
            }


            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterst));
        }

        [HttpPost]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            // This is not neccesary since weare using the [ApiController] attribute at the begging of this controller
            //if(pointOfInterest == null)
            //{
            //    return BadRequest();
            //}

            if (!_cityInfoRepository.DoesCityExist(cityId))
            {
                return NotFound();
            }
            var finalPointOfInterest = _mapper.Map<PointOfInterest>(pointOfInterest);
            _cityInfoRepository.AddPointOfInterestForCity(cityId, finalPointOfInterest);
            _cityInfoRepository.Save();

            var createdPointOfInterest = _mapper.Map<PointOfInterestDto>(finalPointOfInterest);
            return CreatedAtRoute("GetPointOfInterest", new { cityId, id = createdPointOfInterest.Id },
                createdPointOfInterest);

        }

        [HttpPut("{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int pointOfInterestIdToUpdate, [FromBody] PointOfInterestForUpdate pointOfInterestUpdated)
        {

            if (_cityInfoRepository.DoesCityExist(cityId))
            {
                return NotFound();
            }

            var pointOfInterest = _cityInfoRepository.GetCityPointOfInterest(cityId, pointOfInterestIdToUpdate);
            if (pointOfInterest == null)
            {
                return NotFound();
            }
            // When you do this, the first parameters gets the values from the second one, its a way to update using mapping~
            _mapper.Map(pointOfInterest, pointOfInterestUpdated);

            //IMPORTANT
            // Entity framework can update an resource by tracking it, whitch means that a resource can be updated 
            // by searching it in the DB and then changing its chaning its values follow by saving the changes, 
            // but it is a good practice that the Repository implements an update method in case we would like to change 
            // teclogies, so we include it anyway for future cases where we could need it, but again, for updating we could just
            // update the resource directly and then save.
            _cityInfoRepository.UpdatePointOfInterest(cityId, pointOfInterest);
            _cityInfoRepository.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id, [FromBody] JsonPatchDocument<PointOfInterestForUpdate> patchDoc)
        {
            if (!_cityInfoRepository.DoesCityExist(cityId))
            {
                return NotFound();
            }

            var pointOfInterest = _cityInfoRepository.GetCityPointOfInterest(cityId, id);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            var patchOfInterestToPatch = _mapper.Map<PointOfInterestForUpdate>(pointOfInterest);

            patchDoc.ApplyTo(patchOfInterestToPatch, ModelState); //we pass the ModelState in case the consumer made a mistake

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _mapper.Map(patchOfInterestToPatch, pointOfInterest);
            _cityInfoRepository.UpdatePointOfInterest(cityId, pointOfInterest);
            _cityInfoRepository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            if (!_cityInfoRepository.DoesCityExist(cityId))
            {
                return NotFound();
            }

            var pointOfInterest = _cityInfoRepository.GetCityPointOfInterest(cityId, id);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            _cityInfoRepository.DeletePointOfInterest(pointOfInterest);
            _cityInfoRepository.Save();

            return NoContent();
        }
    }
}
