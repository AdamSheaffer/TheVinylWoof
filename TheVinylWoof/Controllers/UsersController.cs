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
using Microsoft.AspNet.Identity;

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
        public IEnumerable<ApplicationUser> Get()
        {
            return _repo.GetProfileUsersIncludingAlbums()
                .OrderBy(n => n.Email)
                .ToList();
        }

        [Route("api/users/{id}")]
        public IEnumerable<ApplicationUser> Get(string id)
        {
            if (id == "currentUser")
            {
                var currentId = User.Identity.GetUserId();
                return _repo.GetProfileUsersIncludingAlbums().Where(x => x.Id == currentId);
            }

            return _repo.GetProfileUsersIncludingAlbums().Where(x => x.Id == id);
        }

        [Route("api/users/{id}")]
        public HttpResponseMessage Post([FromBody] Rating rating)
        {
            if (_repo.AddRating(rating.UserId, rating.newRating) && _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("api/users")]
        public HttpResponseMessage Post(ApplicationUser newUser)
        {
            if (_repo.AddProfileUser(newUser) && _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created, newUser);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

    }
}