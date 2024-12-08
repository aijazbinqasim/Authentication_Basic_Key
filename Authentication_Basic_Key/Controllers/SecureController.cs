using Microsoft.AspNetCore.Mvc;
using Authentication_Basic_Key.Security;

namespace Authentication_Basic_Key.Controllers
{
    [Route("api/secure")]
    [ApiController]
    public class SecureController(IApiKeyValidation apiKeyValidation) : ControllerBase
    {
        private readonly IApiKeyValidation _apiKeyValidation = apiKeyValidation;

        [Route("helloqp")]
        [HttpGet]
        public IActionResult HelloViaQueryParam([FromQuery] string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                return BadRequest();
            
            var isValid = _apiKeyValidation.IsValidApiKey(apiKey);
            if (!isValid)
                return Unauthorized();

            return Ok(new { Greetings = "Welcome you are authorized!" });
        }
    }
}
