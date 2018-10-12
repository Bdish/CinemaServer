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
            try
            {
                _orderRepo = orderRepo;
            }
            catch (Exception)
            {
               // return BadRequest();
            }
        }

        // GET: api/Order
         [HttpGet]
        [Produces("application/xml")]
        public IActionResult Get()
        {
            try
            {
                return Ok(_orderRepo.Get());
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        [Produces("application/xml")]
        public IActionResult Get(int id)
        {
            try
            {
                Order order = _orderRepo.FindById(id);
                if (order == null)
                {
                    return NotFound();
                }

                return Ok(order);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        // POST: api/Order
        [HttpPost]
        public IActionResult Post(/*[FromBody]*/ OrderView orderView)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Order order = new Order { IdSeance = orderView.IdSeance, CountPlace = orderView.CountPlace };
                _orderRepo.Create(order);

                return Ok(order);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, OrderView orderView)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != orderView.Id)
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
                Order order = _orderRepo.FindById(id);

                if (order == null)
                {
                    return NotFound();
                }
                _orderRepo.Remove(order);

                return Ok(order);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [NonAction]
        private bool OrderExists(int id)
        {
            return _orderRepo.Get().Count(x => x.Id == id) > 0;
        }
    }
}
