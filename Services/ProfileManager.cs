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
            var user = this.userDbContext.Users
                .Include(r => r.Claims)
                .SingleOrDefault(r => r.Login == userName);

            return user ?? default(IUser);
        }
    }
}
