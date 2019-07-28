using ProjectSparky.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectSparky.Interfaces
{
    public interface IUserAccountService
    {
        Task<UserAccount> GetUserAccount(string UserAccountGUID);
        Task<List<UserAccount>> GetUserAccounts();
        void AddUserAccount(UserAccount addUserAccount);
        void UpdateUserAccount(UserAccount modUserAccount);
        void DeleteUserAccount(UserAccount delUserAccount);
    }
}
