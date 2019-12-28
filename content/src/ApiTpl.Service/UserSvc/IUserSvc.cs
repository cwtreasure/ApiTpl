namespace ApiTpl.Service.UserSvc
{
    using ApiTpl.Core;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IUserSvc : IBaseService
    {
        Task<ApiSlimResponse> AddUserAsync(AddUserInput input, CancellationToken cancellationToken);
    }
}
