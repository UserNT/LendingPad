using BusinessEntities;
using Core.Services.Users;
using System;
using System.Linq;
using System.Web.Http;
using WebApi.Models.Users;

namespace WebApi.Controllers
{
    [RoutePrefix("users")]
    public class UserController : BaseApiController
    {
        private readonly ICreateUserService _createUserService;
        private readonly IDeleteUserService _deleteUserService;
        private readonly IGetUserService _getUserService;
        private readonly IUpdateUserService _updateUserService;

        public UserController(ICreateUserService createUserService, IDeleteUserService deleteUserService, IGetUserService getUserService, IUpdateUserService updateUserService)
        {
            _createUserService = createUserService;
            _deleteUserService = deleteUserService;
            _getUserService = getUserService;
            _updateUserService = updateUserService;
        }

        [Route("{userId:guid}/create")]
        [HttpPost]
        public IHttpActionResult CreateUser(Guid userId, [FromBody] UserModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _createUserService.Create(userId, model.Name, model.Email, model.Type, model.AnnualSalary, model.Tags, model.Age);
            return Ok(new UserData(user));
        }

        [Route("{userId:guid}/update")]
        [HttpPost]
        public IHttpActionResult UpdateUser(Guid userId, [FromBody] UserModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _getUserService.GetUser(userId);
            if (user == null)
            {
                return NotFound();
            }
            _updateUserService.Update(user, model.Name, model.Email, model.Type, model.AnnualSalary, model.Tags, model.Age);
            return Ok(new UserData(user));
        }

        [Route("{userId:guid}/delete")]
        [HttpDelete]
        public IHttpActionResult DeleteUser(Guid userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _getUserService.GetUser(userId);
            if (user == null)
            {
                return NotFound();
            }
            _deleteUserService.Delete(user);
            return Ok();
        }

        [Route("{userId:guid}")]
        [HttpGet]
        public IHttpActionResult GetUser(Guid userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _getUserService.GetUser(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new UserData(user));
        }

        [Route("list")]
        [HttpGet]
        public IHttpActionResult GetUsers(int skip, int take, UserTypes? type = null, string name = null, string email = null)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var users = _getUserService.GetUsers(type, name, email)
                                       .Skip(skip).Take(take)
                                       .Select(q => new UserData(q))
                                       .ToList();
            return Ok(users);
        }

        [Route("clear")]
        [HttpDelete]
        public IHttpActionResult DeleteAllUsers()
        {
            _deleteUserService.DeleteAll();
            return Ok();
        }

        [Route("list/tag")]
        [HttpGet]
        public IHttpActionResult GetUsersByTag(string tag)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var users = _getUserService.GetUsers(tag)
                                       .Select(q => new UserData(q))
                                       .ToList();
            return Ok(users);
        }
    }
}