using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TheVinylWoof.Models;

namespace TheVinylWoof.Data
{
    public class VinylWoofContext : DbContext
    {
        public VinylWoofContext()
            : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<User> Users { get; set; }
    }
}