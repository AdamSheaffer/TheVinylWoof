using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public IQueryable<Models.Album> GetAlbumsFromUser(int userId)
        {
            return _ctx.Albums.Where(x => x.UserId == userId);
        }


        public IQueryable<Models.User> GetUsers()
        {
            return _ctx.Users;
        }

        public IQueryable<Models.User> GetUserById(int id)
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
    }
}