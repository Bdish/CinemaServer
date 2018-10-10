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
        public Order Get(int id)
        {
            return new Order();
        }

        // POST: api/Order
        [HttpPost]
        public void Post([FromBody] Order value)
        {
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Order value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
