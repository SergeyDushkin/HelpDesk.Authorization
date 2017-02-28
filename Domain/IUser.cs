using System.Collections.Generic;

namespace authorization
{
    public interface IUser
    {
        System.Guid Id { get; set; }
        string Name { get; set; }
        string Login { get; set; }
        string Email { get; set; }

        IEnumerable<IUserClaim> GetClaims();
    }

    public interface IUserClaim
    {
        string Type { get; set; }
        string Value { get; set; }
    }
}
