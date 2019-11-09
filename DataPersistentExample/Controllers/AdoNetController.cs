using DataPersistentExample.Models;
using DataPersistentExample.Repositorys;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataPersistentExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoNetController : ControllerBase
    {
        private readonly IRepository<BusinessObject> _repository;

        public AdoNetController(IEnumerable<IRepository<BusinessObject>> repositorys)
        {
            _repository = repositorys.FirstOrDefault(o => o.GetType().Equals(typeof(AdoNetRepository<BusinessObject>)));
        }

        // GET: api/AdoNet
        [HttpGet]
        public IEnumerable<BusinessObject> Get()
        {
            return _repository.GetAll();
        }

        // GET: api/AdoNet/5
        [HttpGet("{id}", Name = "GetAdoNet")]
        public BusinessObject Get(Guid id)
        {
            return _repository.GetById(id);
        }

        // POST: api/AdoNet
        [HttpPost]
        public void Post([FromBody] dynamic data)
        {
            BusinessObject business = JsonConvert.DeserializeObject<BusinessObject>(data.ToString());
            business.Id = Guid.NewGuid();
            _repository.Create(business);
        }

        // PUT: api/AdoNet/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] dynamic data)
        {
            BusinessObject business = JsonConvert.DeserializeObject<BusinessObject>(data.ToString());
            business.Id = id;
            _repository.Modify(business);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }
    }
}
