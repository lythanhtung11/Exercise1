using Exercise1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise1.Databases
{
    public interface IUserRepository
    {
        // Represents method to get users with password haven't been changed after a period time
        Task<List<User>> GetUsersWithPasswordExpiredAsync(DateTime referenceDate);

        // Represents method to save records in to database
        Task SaveChangesAsync();
    }
}
