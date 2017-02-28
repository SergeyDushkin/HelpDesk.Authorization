using System;

namespace authorization
{
    public class UserClaim : IUserClaim
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

        public UserClaim() {}

        public UserClaim(string type, string value)
        {
            this.Type = type;
            this.Value = value; 
        }
    }
}
