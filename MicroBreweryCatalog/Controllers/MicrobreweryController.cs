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
        
        // GET api/values
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_microbreweryRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Guid guid;
            if (!Guid.TryParse(id, out guid))
            {
                return BadRequest();
            }
            
            var microbrewery = _microbreweryRepository.Get(guid);
            return (microbrewery != null)
                ? Ok(microbrewery)
                : NotFound(id) as IActionResult;
        }

        // GET api/values/5
        [HttpGet("name/{name}")]
        public IActionResult GetByName(string name)
        {
            var microbrewery = _microbreweryRepository.GetByName(name);
            return (microbrewery != null) 
                ? Ok(microbrewery)
                : NotFound(name) as IActionResult;
        }

        [HttpGet("{microbreweryId:guid}/beer")]
        public void AddBeerToBrewery(Guid microbreweryId, [FromBody] Beer newBeer)
        {
            _microbreweryRepository.AddBeer(microbreweryId, newBeer);
        }

        // POST api/values
        [HttpPost]
        public IActionResult AddMicrobrewery([FromBody]Microbrewery microbrewery)
        {
            _microbreweryRepository.Add(microbrewery);
            return Created("microwbrewery", microbrewery);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult UpdateById(Guid id, [FromBody]Microbrewery microbrewery)
        {
            if (id != microbrewery.Id)
            {
                return NotFound(id);
            }

            try
            {
                _microbreweryRepository.Update(microbrewery);
                return Ok(microbrewery);
            }
            catch(InvalidOperationException)
            {
                return NotFound(id);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
           
            if (_microbreweryRepository.Get(id) == null)
            {
                return NotFound(id);
            }

            try
            {
                _microbreweryRepository.Delete(id);
                return Ok();
            }
            catch (InvalidOperationException exc)
            {
                return NotFound(id);
            }
        }
    }
}
