using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain;

namespace MicroBreweryCatalog.Controllers
{
    [Route("api/[controller]")]
    public class MicrobreweryController : Controller
    {
        private IMicrobreweryRepository _microbreweryRepository;

        public MicrobreweryController(IMicrobreweryRepository microbreweryRepository)
        {
            this._microbreweryRepository = microbreweryRepository;
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_microbreweryRepository.GetAll());
        }

        [HttpGet("beers")]
        public IActionResult GetAllBeers()
        {
            return Ok(_microbreweryRepository.GetAllBeers());
        }

        [HttpGet("{microbreweryId}")]
        public IActionResult GetById(string microbreweryId)
        {
            Guid guid;
            if (!Guid.TryParse(microbreweryId, out guid))
            {
                return BadRequest();
            }
            
            var microbrewery = _microbreweryRepository.Get(guid);
            return (microbrewery != null)
                ? Ok(microbrewery)
                : NotFound(microbreweryId) as IActionResult;
        }

        [HttpGet("{microbreweryId:guid}/beers")]
        public IActionResult GetAllBeers(Guid microbreweryId)
        {
            var microbrewery = _microbreweryRepository.Get(microbreweryId);
            return (microbrewery != null)
                ? Ok(microbrewery.Beers)
                : NotFound(microbreweryId) as IActionResult;
        }

        [HttpGet("name/{name}")]
        public IActionResult GetByName(string name)
        {
            var microbrewery = _microbreweryRepository.GetByName(name);
            return (microbrewery != null) 
                ? Ok(microbrewery)
                : NotFound(name) as IActionResult;
        }

        [HttpPost("{microbreweryId:guid}/beer")]
        public IActionResult AddBeerToBrewery(Guid microbreweryId, [FromBody] Beer newBeer)
        {
            _microbreweryRepository.AddBeer(microbreweryId, newBeer);
            return Created("beer", newBeer);
        }

        [HttpPost("{microbreweryId:guid}/beers")]
        public IActionResult AddBeersToBrewery(Guid microbreweryId, HashSet<Beer> beers)
        {
            _microbreweryRepository.AddBeers(microbreweryId, beers);
            return Created("beers", beers);
        }

        [HttpPost]
        public IActionResult AddMicrobrewery([FromBody]Microbrewery microbrewery)
        {
            _microbreweryRepository.Add(microbrewery);
            return Created("microwbrewery", microbrewery);
        }

        [HttpPut("{microbreweryId}")]
        public IActionResult UpdateById(Guid microbreweryId, [FromBody]Microbrewery microbrewery)
        {
            if (microbreweryId != microbrewery.Id)
            {
                return NotFound(microbreweryId);
            }

            try
            {
                _microbreweryRepository.Update(microbrewery);
                return Ok(microbrewery);
            }
            catch(InvalidOperationException)
            {
                return NotFound(microbreweryId);
            }
        }

        [HttpPut("{microbreweryId}/beer/{beerId}")]
        public IActionResult UpdateBeerById(Guid microbreweryId, Guid beerId, [FromBody]Beer beer)
        {
            if (beerId != beer.Id)
            {
                return NotFound(beerId);
            }

            var microbrewery = _microbreweryRepository.Get(microbreweryId);
            if(microbrewery != null)
            {
                return NotFound(microbreweryId);
            }               

            try
            {
                _microbreweryRepository.UpdateBeer(microbreweryId, beer);
                return Ok(beer);
            }
            catch (InvalidOperationException)
            {
                return NotFound(beer);
            }
        }

        [HttpDelete("{microbreweryId}/beer/{beerId}")]
        public IActionResult DeleteBeer(Guid microbreweryId, Guid beerId)
        {
            if (microbreweryId == Guid.Empty || beerId == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                _microbreweryRepository.DeleteBeer(microbreweryId, beerId);
                return Ok();
            }
            catch (InvalidOperationException exc)
            {
                return NotFound((microbreweryId.ToString() + "\\" + beerId.ToString()));
            }
        }

        [HttpDelete("{microbreweryId}")]
        public IActionResult Delete(Guid microbreweryId)
        {
            if (microbreweryId == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                _microbreweryRepository.Delete(microbreweryId);
                return Ok();
            }
            catch (InvalidOperationException exc)
            {
                return NotFound(microbreweryId);
            }
        }
    }
}
