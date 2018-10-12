using CinemaDomain.EFContext;
using CinemaDomain.EFRepository.Implementations;
using CinemaDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaBusinessLogic
{
    public class ManagerSeances: SeanceRepository
    {
        private DbSet<Seance> _seances;

        public ManagerSeances(DbContext context) : base(context)
        {
            _seances = context.Set<Seance>();
        }

        

        public IQueryable<Seance> CurrentSeanceAtDateTime()
        {
            DateTime dateTime = DateTime.Now;
            return _seances.Where(x => x.Start > dateTime);
        }
    }
}
