namespace ApiTpl.V1.Controllers
{
    using ApiTpl.Controllers;
    using ApiTpl.Core;
    using ApiTpl.Service.UserSvc;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;

    [ApiVersion("1")]
    [ApiController]
    [Route("api/user")]
    [Route("api/v{version:apiVersion}/user")]
    public class UserController : ApiTplBaseController
    {
        private readonly IUserSvc _svc;

        public UserController(IUserSvc svc)
        {
            _svc = svc;
        }

        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="req">req</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>Result</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiSlimResponse), 200)]
        [ProducesResponseType(typeof(ApiSlimResponse), 400)]
        public async Task<IActionResult> PostAsync([FromBody] AddUserReq req, CancellationToken cancellationToken)
        {
            var (code, msg) = req.Valid();

            if (code != ApiReturnCode.Succeed) return BadRequest(ApiSlimResponse.GetResult(code, msg));

            var res = await _svc.AddUserAsync(req.BuildAddUserInput(TraceId, UserIp), cancellationToken);

            return Ok(res);
        }
    }
}
