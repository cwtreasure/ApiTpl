namespace ApiTpl.Service.UserSvc
{
    using ApiTpl.Core;
    using ApiTpl.Core.Repositories;
    using Microsoft.Extensions.Logging;
    using System.Threading;
    using System.Threading.Tasks;

    public class UserSvc : IUserSvc
    {
        private readonly ILogger _logger;
        private readonly IUserRepo _userRepo;

        public UserSvc(
            ILoggerFactory loggerFactory,
            IUserRepo userRepo)
        {
            _logger = loggerFactory.CreateLogger<UserSvc>();
            _userRepo = userRepo;
        }

        public async Task<ApiSlimResponse> AddUserAsync(AddUserInput input, CancellationToken cancellationToken)
        {
            var user = input.BuildUser();

            var flag = await _userRepo.AddAsync(user);

            return flag
                ? ApiSlimResponse.GetSucceed("添加成功")
                : ApiSlimResponse.GetResult(ApiReturnCode.OperationFail, "添加失败");
        }
    }
}
