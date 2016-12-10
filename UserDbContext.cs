using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace authorization
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    [Table("WH_USERS")]
    public class User : IUser
    {
        [Key]
        public Guid GUID_RECORD { get; set; }
        public string FULL_NAME { get; set; }
        public string LOGIN { get; set; }

        [NotMapped]
        public string Id { get { return this.GUID_RECORD.ToString(); } set { this.GUID_RECORD = Guid.Parse(value); } }

        [NotMapped]
        public string Name { get { return this.FULL_NAME; } set { this.FULL_NAME = value; } }

        [NotMapped]
        public string Login { get { return this.LOGIN; } set { this.LOGIN = value; } }

        [NotMapped]
        public string Email { get; set; }

        [NotMapped]
        public string Phone { get; set; }

        [NotMapped]
        public string[] Roles { get; set; }

        [ForeignKey("USER_GUID")]
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }

    [Table("WH_ROLES")]
    public class Role
    {
        [Key]
        public Guid GUID_RECORD { get; set; }
        public string ROLE_NAME { get; set; }
        public string DESCRIPTION { get; set; }
    }

    [Table("WH_USER_ROLES")]
    public class UserRole
    {
        [Key]
        public Guid GUID_RECORD { get; set; }
        public Guid USER_GUID { get; set; }
        public Guid ROLE_GUID { get; set; }

        [ForeignKey("ROLE_GUID")]
        public virtual Role Role { get; set; }
    }
}
