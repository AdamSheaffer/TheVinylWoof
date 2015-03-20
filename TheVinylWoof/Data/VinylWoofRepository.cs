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
            return _ctx.Albums.Where(x => x.UserId == userId);
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
                //Log this error
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
                //Log error
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
                //Log error
                return false;
            }
        }

        public IQueryable<ApplicationUser> GetSeller(int albumId)
        {
            var SellerId = _ctx.Albums.Where(x => x.Id == albumId).First().UserId;
            return _ctx.Users.Where(x => x.Id == SellerId);
        }



        public bool Swap(Album album, string buyerId)
        {
            throw new NotImplementedException();
        }
    }
}