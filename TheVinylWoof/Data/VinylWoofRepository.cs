using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheVinylWoof.Models;
using System.Data.Entity;

namespace TheVinylWoof.Data
{
    public class VinylWoofRepository : IVinylWoofRepository
    {
        VinylWoofContext _ctx;

        public VinylWoofRepository(VinylWoofContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Models.Album> GetAlbums()
        {
            return _ctx.Albums;
        }

        public IQueryable<Models.Album> GetAlbumsFromUser(string userId)
        {
            return _ctx.Albums.Where(x => x.UserId == userId && x.IsSold == false);
        }


        public IQueryable<Models.ApplicationUser> GetProfileUsers()
        {
            return _ctx.Users.Include("Albums");
        }

        public IQueryable<Models.ApplicationUser> GetProfileUserById(string id)
        {
            return _ctx.Users.Where(x => x.Id == id);
        }


        public bool Save()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool AddAlbum(Models.Album newAlbum)
        {
            try
            {
                _ctx.Albums.Add(newAlbum);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }


        public IQueryable<ApplicationUser> GetProfileUsersIncludingAlbums()
        {
            return _ctx.Users.Include("Albums");
        }


        public bool AddProfileUser(Models.ApplicationUser newUser)
        {
            try
            {
                _ctx.Users.Add(newUser);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public IQueryable<ApplicationUser> GetSeller(int albumId)
        {
            var SellerId = _ctx.Albums.Where(x => x.Id == albumId).First().UserId;
            return _ctx.Users.Where(x => x.Id == SellerId);
        }



        public bool Swap(int albumId, string buyerId)
        {
            if (buyerId == null)
            {
                return false;
            }
            var album = _ctx.Albums.Where(a => a.Id == albumId).First();
            var seller = _ctx.Users.Where(u => u.Id == album.UserId).First();
            var buyer = _ctx.Users.Where(u => u.Id == buyerId).First();
            bool hasEnoughCredits = buyer.Credits >= album.Cost;

            if (hasEnoughCredits)
            {
                try
                {
                    seller.Credits += album.Cost;
                    buyer.Credits -= album.Cost;
                    album.BuyerId = buyerId;
                    album.IsSold = true;
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
            }
            return false;
        }


        public IQueryable<Album> GetAlbumsByTitleAndGenre(string title, string genre)
        {
            var albums = _ctx.Albums.Where(a => a.IsSold == false);

            if (!String.IsNullOrEmpty(title))
            {
                albums = albums.Where(s => s.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                albums = albums.Where(x => x.Genre == genre);
            }

            return albums;
        }


        public IQueryable<Album> GetAlbumsBought(string userId)
        {
            return _ctx.Albums.Where(a => a.BuyerId == userId);
        }


        public IQueryable<Album> GetAlbumsSold(string userId)
        {
            return _ctx.Albums.Where(a => a.UserId == userId && a.IsSold == true);
        }


        public IQueryable<Album> GetAvailableAlbums()
        {
            return _ctx.Albums.Where(a => a.IsSold == false);
        }


        public bool AddRating(string userId, int rating)
        {
            var user = _ctx.Users.Where(u => u.Id == userId).First();
            try
            {
                user.Rating = NewAverageRating(user.Rating, user.NumRatings, rating);
                user.NumRatings += 1;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        private decimal NewAverageRating(decimal avgRating, int numRatings, int newRating)
        {
            decimal sumRatings = avgRating * numRatings + newRating;
            int updatedNumRatings = numRatings + 1;
            return sumRatings / updatedNumRatings;
        }
    }
}