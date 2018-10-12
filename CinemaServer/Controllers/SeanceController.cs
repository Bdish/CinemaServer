using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CinemaBusinessLogic;
using CinemaDomain.EFRepository.Interfaces;
using CinemaDomain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeanceController : ControllerBase
    {
        private ManagerSeances _seanceRepo;

        public SeanceController(ManagerSeances seanceRepo)
        {
            _seanceRepo = seanceRepo;
        }

        // GET: api/Seance
        [HttpGet]
        public IActionResult/*IEnumerable<Seance>*/ Get()
        {
            try
            {
                //return Ok(_seanceRepo.Get());
                return Ok(_seanceRepo.CurrentSeanceAtDateTime());
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        // GET: api/Seance/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            try
            {
                Seance seance = _seanceRepo.FindById(id);
                if (seance == null)
                {
                    return NotFound();
                }

                return Ok(seance);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        // POST: api/Seance
        [HttpPost]
        
        public IActionResult Post(/*[FromBody]*/ SeanceView seanceView)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Seance seance = new Seance { Name = seanceView.Name, Start = (DateTime)seanceView.Start };

                _seanceRepo.Create(seance);

                return Ok(seance);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        // PUT: api/Seance/5
        [HttpPut("{id}")]
        public IActionResult Put(int id,/* [FromBody]*/SeanceView seanceView)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != seanceView.Id)
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return BadRequest();
            }

            try
            {
                Seance  seanceOld = _seanceRepo.FindById(id);
                seanceOld.Name = seanceView.Name;
                seanceOld.Start =(DateTime) seanceView.Start;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeanceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }
            
            return StatusCode((int)HttpStatusCode.NoContent);

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Seance delSeance = _seanceRepo.FindById(id);

                if (delSeance == null)
                {
                    return NotFound();
                }

                _seanceRepo.Remove(delSeance);

                return Ok(delSeance);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [NonAction]
        private bool SeanceExists(int id)
        {
            return _seanceRepo.Get().Count(x => x.Id == id) > 0;
        }
    }
}
