namespace ApiTpl.Core.Repositories
{
    using ApiTpl.Core.Domains;
    using System.Threading.Tasks;

    public interface IUserRepo : IBaseRepo
    {
        Task<bool> AddAsync(User entity);
    }
}
