using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace authorization
{
    public class ProfileManager : IProfileManager
    {
        private readonly UserDbContext userDbContext;

        public ProfileManager(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public IUser Find(string userName, string password)
        {
            var user = this.userDbContext.Users.SingleOrDefault(r => r.LOGIN == userName);

            if (user == null)
                return null;

            user.Roles = this.userDbContext.UserRoles
                .Include(r => r.Role)
                .Where(r => r.USER_GUID == user.GUID_RECORD)
                .Select(r => r.Role.ROLE_NAME)
                .ToArray();

            return user;
        }
    }
}
