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
        IQueryable<Album> GetAlbumsByTitleAndGenre(string title, string genre);
        IQueryable<Album> GetAlbumsFromUser(string userId);
        IQueryable<ApplicationUser> GetProfileUsers();
        IQueryable<ApplicationUser> GetProfileUserById(string id);
        IQueryable<ApplicationUser> GetProfileUsersIncludingAlbums();
        IQueryable<ApplicationUser> GetSeller(int albumId);
        bool Save();
        bool AddAlbum(Album newAlbum);
        bool AddProfileUser(ApplicationUser newUser);
        bool Swap(int albumId, string buyerId);
    }
}
