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
    public class EfController : ControllerBase
    {
        private readonly IRepository<BusinessObject> _repository;

        public EfController(IEnumerable<IRepository<BusinessObject>> repositorys)
        {
            _repository = repositorys.FirstOrDefault(o => o.GetType().Equals(typeof(EfRepository<BusinessObject>))); ;
        }

        // GET: api/Ef
        [HttpGet]
        public IEnumerable<BusinessObject> Get()
        {
            return _repository.GetAll();
        }

        // GET: api/Ef/5
        [HttpGet("{id}", Name = "GetEf")]
        public BusinessObject Get(Guid id)
        {
            return _repository.GetById(id);
        }

        // POST: api/Ef
        [HttpPost]
        public void Post([FromBody] dynamic data)
        {
            BusinessObject business = JsonConvert.DeserializeObject<BusinessObject>(data.ToString());
            _repository.Create(business);
        }

        // PUT: api/Ef/5
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
