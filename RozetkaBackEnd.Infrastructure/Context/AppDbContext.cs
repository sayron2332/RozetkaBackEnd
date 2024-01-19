using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RozetkaBackEnd.Core.Entites.User;
using RozetkaBackEnd.Infrastructure.Initializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RozetkaBackEnd.Infrastructure.Context
{
    internal class AppDbContext : IdentityDbContext
    {
        public AppDbContext() : base() { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoleAndUser.SeedRolesAndUsers(builder);
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        DbSet<User> Users { get; set; }
    }
}
