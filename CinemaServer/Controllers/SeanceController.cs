using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaDomain.EFRepository.Interfaces;
using CinemaDomain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeanceController : ControllerBase
    {
        private IGenericRepository<Seance> _seanceRepo;

        public SeanceController(IGenericRepository<Seance> seanceRepo)
        {
            _seanceRepo = seanceRepo;
        }

        // GET: api/Seance
        [HttpGet]
        public IEnumerable<Seance> Get()
        {
            return _seanceRepo.Get();
        }

        // GET: api/Seance/5
        [HttpGet("{id}", Name = "Get")]
        public Seance Get(int id)
        {
            return _seanceRepo.FindById(id);
        }

        // POST: api/Seance
        [HttpPost]
        public void Post([FromBody] Seance seance)
        {
             _seanceRepo.Create(seance);
        }

        // PUT: api/Seance/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Seance seance)
        {
            if (id == seance.Id)
            {
                _seanceRepo.Update(seance);
            }
           
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Seance delSeance = _seanceRepo.FindById(id);
            _seanceRepo.Remove(delSeance);
        }
    }
}
