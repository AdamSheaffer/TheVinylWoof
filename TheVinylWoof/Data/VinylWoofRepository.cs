using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheVinylWoof.Models;

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


        public IQueryable<Models.ApplicationUser> GetUsers()
        {
            return _ctx.ProfileUsers;
        }

        public IQueryable<Models.ApplicationUser> GetUserById(string id)
        {
            return _ctx.ProfileUsers.Where(x => x.Id == id);
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


        public IQueryable<ApplicationUser> GetUsersIncludingAlbums()
        {
            return _ctx.ProfileUsers.Include("Albums");
        }


        public bool AddUser(Models.ApplicationUser newUser)
        {
            try
            {
                _ctx.ProfileUsers.Add(newUser);
                return true;
            }
            catch (Exception ex)
            {
                //Log error
                return false;
            }
        }
    }
}