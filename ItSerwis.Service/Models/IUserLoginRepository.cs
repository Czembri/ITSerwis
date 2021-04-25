using ItSerwis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItSerwis.Service.Models
{
    public interface IUserLoginRepository
    {
        Task<IEnumerable<UserLogin>> GetUsers();
        Task<UserLogin> GetUser(int applicantID);
        Task<UserLogin> GetUserByName(string name);
        Task<UserLogin> AddUser(UserLogin user);
        Task<UserLogin> UpdateUser(UserLogin user);
    }
}
