namespace ApiTpl.Data
{
    using ApiTpl.Core.Domains;
    using ApiTpl.Core.Repositories;
    using Microsoft.Extensions.Configuration;
    using System.Threading.Tasks;

    public class UserRepo : DapperRepositoryBase, IUserRepo
    {
        public UserRepo(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<bool> AddAsync(User entity)
        {
            // var row = await ExecuteAsync(SqlCmds.User_Add, entity);
            // return row > 0;
            return await Task.FromResult(true);
        }
    }
}
