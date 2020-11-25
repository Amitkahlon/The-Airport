using Airport_DAL.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_DAL.Context
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./Database.sqlite");
        }

        public DbSet<Airport> Airports { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<PlaneLog> PlanesLog { get; set; }


        //DbSet<Airport>
        //DBSet<Station>
        //DBSet<Route>
        //DBSet<Plane>

    }
}
