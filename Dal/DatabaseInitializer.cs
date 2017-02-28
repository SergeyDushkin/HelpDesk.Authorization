using System.Threading.Tasks;

namespace authorization.Dal
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }

    public class DatabaseInitializer : IDatabaseSeeder
    {
        private readonly UserDbContext context;

        public DatabaseInitializer(UserDbContext context)
        {
            this.context = context;
        }

        public async Task SeedAsync()
        {
            var test = new User("test", "test", new UserClaim("role", "user"));
            var admin = new User("admin", "admin", new UserClaim("role", "admin"));

            var sdushkin = new User("Душкин Сергей Анатольевич", "sdushkin", 
                new UserClaim("role", "admin"), 
                new UserClaim("feature", "ticket:view"), 
                new UserClaim("feature", "ticket:create"), 
                new UserClaim("feature", "ticket:update"), 
                new UserClaim("feature", "ticket:delete"));

            var srodionov = new User("Родионов Сергей", "srodionov", 
                new UserClaim("role", "admin"), 
                new UserClaim("feature", "ticket:view"), 
                new UserClaim("feature", "ticket:create"), 
                new UserClaim("feature", "ticket:update"));
            
            context.Add(test);
            context.Add(admin);
            context.Add(sdushkin);
            context.Add(srodionov);
            
            await context.SaveChangesAsync();
        }
    }
}