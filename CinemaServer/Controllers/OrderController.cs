using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
    public class OrderController : ControllerBase
    {
        private IGenericRepository<Order> _orderRepo;

        public OrderController(IGenericRepository<Order> orderRepo)
        {
            _orderRepo = orderRepo;
        }

        // GET: api/Order
         [HttpGet]
        [Produces("application/xml")]
        public IEnumerable<Order> Get()
        {
            return new Order[] { new Order() };
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        [Produces("application/xml")]
        public IActionResult Get(int id)
        {
            Order order = _orderRepo.FindById(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // POST: api/Order
        [HttpPost]
        public IActionResult Post(/*[FromBody]*/ OrderView orderView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order order = new Order { IdSeance = orderView.IdSeance, CountPlace = orderView.CountPlace };
            _orderRepo.Create(order);

            return Ok();
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, OrderView orderView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderView.Id)
            {
                return BadRequest();
            }

            try
            {
                Order orderOld = _orderRepo.FindById(id);
                orderOld.IdSeance = orderView.IdSeance;
                orderOld.CountPlace =orderView.CountPlace;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode((int)HttpStatusCode.NoContent);
            
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Order order = _orderRepo.FindById(id);

            if (order == null)
            {
                return NotFound();                
            }
            _orderRepo.Remove(order);

            return Ok(order);
        }

        private bool OrderExists(int id)
        {
            return _orderRepo.Get().Count(x => x.Id == id) > 0;
        }
    }
}
