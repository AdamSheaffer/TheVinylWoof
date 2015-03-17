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
        IQueryable<ApplicationUser> GetUsers();
        IQueryable<ApplicationUser> GetUserById(string id);
        IQueryable<ApplicationUser> GetUsersIncludingAlbums();
        bool Save();
        bool AddAlbum(Album newAlbum);
        bool AddUser(ApplicationUser newUser);
    }
}
