using JsonFruit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRestServer.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyRestServer.Controllers
{
    // api/Fruits
    [Route("api/[controller]")]
    [ApiController]
    public class FruitsController : ControllerBase
    {
        private FruitManager _manager = new FruitManager();
        // GET: api/Fruits?substring=banan
        [HttpGet]
        public IEnumerable<Fruit> Get([FromQuery] string substring)
        {
            return _manager.GetAll(substring);
        }

        // GET api/fruits/1
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("{substring}")]
        public ActionResult<IEnumerable<Fruit>> GetWithSubstring(string substring)
        {
            List<Fruit> result = _manager.GetAll(substring);
            if (result.Count <= 0)
            {
                return NoContent();
            } else
            {
                return Ok(result);
            }
        }

        // POST api/<FruitsController>
        [HttpPost]
        public void Post([FromBody] Fruit value)
        {
        }

        // PUT api/Fruits/1
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Fruit value)
        {
        }

        // DELETE api/<FruitsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
