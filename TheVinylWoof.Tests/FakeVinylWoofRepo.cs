using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheVinylWoof.Data;

namespace TheVinylWoof.Tests
{
    class FakeVinylWoofRepo : IVinylWoofRepository
    {
        public IQueryable<Models.Album> GetAlbums()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Models.Album> GetAlbumsFromUser(int userId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Models.User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Models.User> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Models.User> GetUsersIncludingAlbums()
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool AddAlbum(Models.Album newAlbum)
        {
            throw new NotImplementedException();
        }

        public bool AddUser(Models.User newUser)
        {
            throw new NotImplementedException();
        }
    }
}
