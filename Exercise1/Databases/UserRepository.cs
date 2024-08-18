using Exercise1.Models;
using Microsoft.EntityFrameworkCore;

namespace Exercise1.Databases
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        // Represents method to get users with password haven't been changed after a period time
        public async Task<List<User>> GetUsersWithPasswordExpiredAsync(DateTime referenceDate)
        {
            var users = await _context.Users
                .Where(u => u.LastUpdatePwd < referenceDate && u.Status != UserStatus.REQUIRE_CHANGE_PWD)
                .ToListAsync();

            return users;
        }

        // Represents method to save records in to database
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
