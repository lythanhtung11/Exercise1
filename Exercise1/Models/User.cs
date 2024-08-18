using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise1.Models
{
    public class User
    {
        public required string Id { get; set; }
        public required string Email { get; set; }
        public UserStatus Status { get; set; }
        public DateTime LastUpdatePwd { get; set; }
    }

    public enum UserStatus
    {
        REQUIRE_CHANGE_PWD = 1,
        PWS_CHANGED = 2
    }

}
