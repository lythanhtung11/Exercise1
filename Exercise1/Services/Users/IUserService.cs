using Exercise1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise1.Services.Users
{
    internal interface IUserService
    {
        // Represents method to update user status
        Task UpdateUserStatusAsync();
    }
}
