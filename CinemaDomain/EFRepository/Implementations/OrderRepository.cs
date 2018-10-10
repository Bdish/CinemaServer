using CinemaDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaDomain.EFRepository.Implementations
{
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository(DbContext context) : base(context)
        {
        }
    }
}
