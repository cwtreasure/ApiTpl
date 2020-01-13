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
            // // 1.
            // var row = await ExecuteAsync(SqlCmds.User_Add, entity);
            // return row > 0;
            // // 2.
            // await this.AddAsync(entity, "user");
            return await Task.FromResult(true);
        }
    }
}
