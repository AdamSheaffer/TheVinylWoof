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
using TheVinylWoof.Data;
using TheVinylWoof.Models;

namespace TheVinylWoof.Controllers
{
    public class UsersController : ApiController
    {
        private IVinylWoofRepository _repo;

        public UsersController(IVinylWoofRepository repo)
        {
            _repo = repo;
        }

        [Route("api/users")]
        public IEnumerable<User> Get()
        {
            return _repo.GetUsersIncludingAlbums()
                .OrderBy(n => n.Username)
                .ToList();
        }

        [Route("api/users/{id}")]
        public User Get(string id)
        {
            return _repo.GetUsersIncludingAlbums().Where(x => x.Id == id).First();
        }

        [Route("api/users")]
        public HttpResponseMessage Post(User newUser)
        {
            if (_repo.AddUser(newUser) && _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created, newUser);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}