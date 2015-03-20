using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Security;
using TheVinylWoof.Data;
using TheVinylWoof.Models;
using Microsoft.AspNet.Identity;

namespace TheVinylWoof.Controllers
{
    public class AlbumsController : ApiController
    {
        private IVinylWoofRepository _repo;

        public AlbumsController(IVinylWoofRepository repo)
        {
            _repo = repo;
        }

        [Route("api/albums")]
        public IEnumerable<Album> Get()
        {
            return _repo.GetAlbums()
                    .OrderByDescending(n => n.Genre)
                    .ToList();
        }

        [Route("api/albums/{id}")]
        public IEnumerable<Album> Get(int id)
        {
            var albums = _repo.GetAlbums();

            if (id != 0)
            {
                albums = albums.Where(i => i.Id == id);
            }

            return albums;
        }

        [Route("api/users/{profileid}/albums")]
        public IEnumerable<Album> GetUserAlbums(string profileid)
        {
            return _repo.GetAlbums().Where(a => a.UserId == profileid);
        }

        [Route("api/albums/{albumid}/user")]
        public IEnumerable<ApplicationUser> GetSeller(int albumId)
        {
            return _repo.GetSeller(albumId);
        }

        [Route("api/albums")]
        public HttpResponseMessage Post([FromBody]Album newAlbum)
        {
            newAlbum.UserId = User.Identity.GetUserId();

            if (_repo.AddAlbum(newAlbum) && _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created, newAlbum);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("api/albums/{albumid}")]
        public HttpResponseMessage PostGrab(int albumid)
        {
            throw new NotImplementedException();    
        }
    }
}