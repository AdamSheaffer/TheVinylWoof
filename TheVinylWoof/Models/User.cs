using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheVinylWoof.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int Credits { get; set; }
        public decimal Rating { get; set; }
        public int NumRatings { get; set; }
        public string Bio { get; set; }
        public ICollection<Album> Albums { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
    }
    
}