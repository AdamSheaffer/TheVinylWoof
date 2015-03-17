using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheVinylWoof.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public virtual int UserId { get; set; }
        public int BuyerId { get; set; }
        public int Cost { get; set; }
        public bool IsSold { get; set; }
        public string CoverLocation { get; set; }
        public string Condition { get; set; }
    }
}