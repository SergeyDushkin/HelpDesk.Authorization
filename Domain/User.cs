using System.Collections.Generic;
using System;

namespace authorization
{
    public class User : IUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public ICollection<UserClaim> Claims { get; set; }

        public User() {}

        public User(string name, string login, string email = null)
        {
            this.Name = name;
            this.Login = login; 
            this.Email = email;
        }

        public User(string name, string login, string email, params UserClaim[] claims)
        {
            this.Name = name;
            this.Login = login; 
            this.Email = email;

            this.Claims = new List<UserClaim>(claims);
        }

        public User(string name, string login, params UserClaim[] claims)
        {
            this.Name = name;
            this.Login = login; 

            this.Claims = new List<UserClaim>(claims);
        }

        public IEnumerable<IUserClaim> GetClaims()
        {
            return this.Claims;
        }
    }
}
