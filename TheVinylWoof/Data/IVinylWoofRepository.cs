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
        IQueryable<Album> GetAlbumsFromUser(int userId);
        IQueryable<User> GetUsers();
        IQueryable<User> GetUserById(int id);
    }
}
