using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheVinylWoof.Models
{
    public class Rating
    {
        public string UserId { get; set; }
        public int newRating { get; set; }

        public Rating(string _userid, int _newrating)
        {
            UserId = _userid;
            newRating = _newrating;
        }
    }
}