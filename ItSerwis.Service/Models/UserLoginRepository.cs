using ItSerwis.Model;
using ItSerwis_Merge_v2;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItSerwis.Service.Models
{
    public class UserLoginRepository : IUserLoginRepository
    {
        private readonly AppDbContext appDbContext;

        public UserLoginRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<UserLogin> AddUser(UserLogin user)
        {
            var enc = new Encryptor();
            user.PasswordHash = enc.EncryptData(user.PasswordHash);
            user.LoginHash = enc.EncryptData(user.LoginHash);
            var result = await appDbContext.UserLogin.AddAsync(user);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<UserLogin> GetUser(int UserID)
        {
            return await appDbContext.UserLogin
                .FirstOrDefaultAsync(e => e.UserID == UserID);
        }

        public async Task<IEnumerable<UserLogin>> GetUsers()
        {
            return await appDbContext.UserLogin.ToListAsync();
        }

        public async Task<UserLogin> GetUserByName(string name)
        {
            return await appDbContext.UserLogin.FirstOrDefaultAsync(e => e.FirstName == name || e.LastName == name);
        }

        public async Task<UserLogin> UpdateUser(UserLogin user)
        {
            var enc = new Encryptor();
            var result = await appDbContext.UserLogin.FirstOrDefaultAsync(e => e.UserID == user.UserID);
            if (result != null)
            {
                result.FirstName = user.FirstName;
                result.LastName = user.LastName;
                result.PasswordHash = enc.EncryptData(user.PasswordHash);
                result.LoginHash = enc.EncryptData(user.LoginHash);
                result.Age = user.Age;

                await appDbContext.SaveChangesAsync();
                return result;
            }
            return result;
        }

    }
}
