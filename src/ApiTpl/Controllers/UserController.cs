namespace ApiTpl.Controllers
{
    using ApiTpl.Core;
    using ApiTpl.Service.UserSvc;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/user")]
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
        public async Task<ApiSlimResponse> PostAsync([FromBody] AddUserReq req, CancellationToken cancellationToken)
        {
            var (code, msg) = req.Valid();

            if (code != ApiReturnCode.Succeed) return ApiSlimResponse.GetResult(code, msg);

            var res = await _svc.AddUserAsync(req.BuildAddUserInput(TraceId, UserIp), cancellationToken);

            return res;
        }
    }
}
