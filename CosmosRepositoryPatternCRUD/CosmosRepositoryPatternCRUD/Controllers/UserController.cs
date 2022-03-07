using CosmosRepositoryPatternCRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CosmosRepository;
using Polly;
using Polly.Retry;

namespace CosmosRepositoryPatternCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ILogger<UserController> _logger;
        IRepository<User> _userRepository;
        AsyncRetryPolicy<ActionResult> _retryPolicy;
        public UserController(ILogger<UserController> logger, IRepository<User> userRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
            _retryPolicy = Policy<ActionResult>.Handle<HttpRequestException>().RetryAsync(retryCount: 5);
        }
        [Route("user/signup")]
        [HttpPost]
        public async Task<ActionResult> AddUser([FromForm] User user)
        {
            user.Id = Guid.NewGuid().ToString();
            user.Type = "User";
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                var res = await _userRepository.CreateAsync(user);
                if (res != null) return Ok(new { Code = "200", Status = "Ok", Data = "User Added" });
                return BadRequest(new { Code = "400", Status = "BadRequest", Data = "Unable to add user!" });
            });
        }
        [Route("get/user")]
        [HttpGet]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                var res = await _userRepository.GetAsync(id);
                if (res != null) return Ok(new { Code = "200", Status = "Ok", Data = res });
                return NotFound(new { Code = "404", Status = "NotFound", Data = "User Not Found" });
            });
        }
    }
}
