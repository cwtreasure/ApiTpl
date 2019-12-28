namespace ApiTpl.Service.UserSvc
{
    using ApiTpl.Core;
    using ApiTpl.Core.ApiClients;
    using ApiTpl.Core.Repositories;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class UserSvc : IUserSvc
    {
        private readonly ILogger _logger;
        private readonly IUserRepo _userRepo;
        private readonly IDemoApi _demoApi;

        public UserSvc(
            ILoggerFactory loggerFactory,
            IUserRepo userRepo,
            IDemoApi demoApi)
        {
            _logger = loggerFactory.CreateLogger<UserSvc>();
            _userRepo = userRepo;
            _demoApi = demoApi;
        }

        public async Task<ApiSlimResponse> AddUserAsync(AddUserInput input, CancellationToken cancellationToken)
        {
            var user = input.BuildUser();

            var demo = await _demoApi.GetDemoByIdAsync(1, TimeSpan.FromSeconds(3));

            var flag = await _userRepo.AddAsync(user);

            return flag
                ? ApiSlimResponse.GetSucceed("添加成功")
                : ApiSlimResponse.GetResult(ApiReturnCode.OperationFail, "添加失败");
        }
    }
}
