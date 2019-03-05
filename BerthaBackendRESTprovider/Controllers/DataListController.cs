using System.Collections.Generic;
using System.Linq;
using BerthaBackendRESTprovider.model;
using Microsoft.AspNetCore.Mvc;

namespace BerthaBackendRESTprovider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataListController : ControllerBase
    {
        private static readonly List<ExtendedMeasurement> data = new List<ExtendedMeasurement>();
        // GET: api/Data
        [HttpGet]
        public IEnumerable<ExtendedMeasurement> Get()
        {
            return data;
        }

        // GET: api/Data/5
        [HttpGet("{userid}", Name = "Get")]
        public IEnumerable<ExtendedMeasurement> GetByUserId(string userid)
        {
            return data.Where(m => m.UserId == userid).OrderBy(m => m.Utc);
        }

        // GET: api/Data/5
        [Route("{userid}/{after}")]
        public IEnumerable<ExtendedMeasurement> GetByUserIdAfter(string userid, int after)
        {
            return data.Where(m => m.UserId == userid && m.Utc >= after).OrderBy(m => m.Utc);
        }


        // POST: api/Data
        [HttpPost]
        public int Post([FromBody] ExtendedMeasurement value)
        {
            data.Add(value);
            return data.Count;

        }

        // PUT: api/Data/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
