using CinemaDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaDomain.EFRepository.Implementations
{
    public class SeanceRepository : GenericRepository<Seance>
    {
        public SeanceRepository(DbContext context) : base(context)
        {
        }
    }
}
