using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TheVinylWoof.Models;

namespace TheVinylWoof.Data
{
    public class VinylWoofContext : IdentityDbContext<ApplicationUser>
    {
        public VinylWoofContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public static VinylWoofContext Create()
        {
            return new VinylWoofContext();
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<ApplicationUser> ProfileUsers { get; set; }
    }
}

