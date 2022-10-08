using ClienteServidor.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteServidor.DAL
{
     class Contexto : DbContext
    {
        public DbSet<Apartado> Apartados { get; set; }
        public object Reservaciones { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=Data\Reservaciones.db");
        }
      
    }
}

