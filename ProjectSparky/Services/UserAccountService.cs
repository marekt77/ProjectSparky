using Microsoft.EntityFrameworkCore;
using ProjectSparky.Interfaces;
using ProjectSparky.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectSparky.Services
{
    public class UserAccountService : IUserAccountService
    {
        public async Task<UserAccount> GetUserAccount(string UserAccountGUID)
        {
            UserAccount rUA = null;

            using (var db = new ProjectSparyContext())
            {
                rUA = await db.UserAccounts.FirstOrDefaultAsync(x => x.UserAccountGUID == UserAccountGUID);
            }

            return rUA;
        }

        public async Task<List<UserAccount>> GetUserAccounts()
        {
            List<UserAccount> listUA = null;

            using (var db = new ProjectSparyContext())
            {
                listUA = await db.UserAccounts.ToListAsync();
            }

            return listUA;
        }

        public async void AddUserAccount(UserAccount addUserAccount)
        {
            using (var db = new ProjectSparyContext())
            {
                db.UserAccounts.Add(addUserAccount);
                await db.SaveChangesAsync();
            }
        }

        public async void UpdateUserAccount(UserAccount modUserAccount)
        {
            using (var db = new ProjectSparyContext())
            {
                db.UserAccounts.Update(modUserAccount);
                await db.SaveChangesAsync();
            }
        }

        public async void DeleteUserAccount(UserAccount delUserAccount)
        {
            using (var db = new ProjectSparyContext())
            {
                db.UserAccounts.Remove(delUserAccount);
                await db.SaveChangesAsync();
            }
        }
    }
}
