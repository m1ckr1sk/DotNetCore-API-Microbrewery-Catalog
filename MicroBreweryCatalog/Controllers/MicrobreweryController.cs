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
        public IActionResult Get()
        {
            return Ok(_microbreweryRepository.GetAll());
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

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Microbrewery microbrewery)
        {
            _microbreweryRepository.Add(microbrewery);
            return Created("microwbrewery", microbrewery);
        }

        // PUT api/values/5
        [HttpPut("{guid}")]
        public IActionResult UpdateById(Guid guid, [FromBody]Microbrewery microbrewery)
        {
            if( guid != microbrewery.Id)
            {
                return NotFound(guid);
            }

            try
            {
                _microbreweryRepository.Update(microbrewery);
                return Ok(microbrewery);
            }
            catch(InvalidOperationException exc)
            {
                return NotFound(guid);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
           
            if (_microbreweryRepository.Get(guid);)
            {
                return NotFound(guid);
            }

            try
            {
                _microbreweryRepository.Delete(microbrewery);
                return Ok();
            }
            catch (InvalidOperationException exc)
            {
                return NotFound(guid);
            }
        }
    }
}
