using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheVinylWoof.Models;

namespace TheVinylWoof.Data
{
    public interface IVinylWoofRepository
    {
        IQueryable<Album> GetAlbums();
        IQueryable<Album> GetAlbumsFromUser(string userId);
        IQueryable<User> GetUsers();
        IQueryable<User> GetUserById(string id);
        IQueryable<User> GetUsersIncludingAlbums();
        bool Save();
        bool AddAlbum(Album newAlbum);
        bool AddUser(User newUser);
    }
}
