using System.Threading.Tasks;

namespace AuctimoTraders.Interfaces
{
    /// <summary>
    ///     Interface contains tasks to handle different Role operations
    /// </summary>
    public interface IAppRoleService
    {
        /// <summary>
        ///     Create roles
        /// </summary>
        /// <returns></returns>
        Task CreateRolesAsync();
    }
}