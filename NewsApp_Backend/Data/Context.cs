﻿using Microsoft.EntityFrameworkCore;
using NewsApp.Models;

namespace NewsApp.Data
{
	public class Context : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<News> News { get; set; }

		public Context(DbContextOptions<Context> options)
		: base(options)
		{
			Database.EnsureCreated();
		}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin@gmail.com";
            string adminPassword = "123qwe";

            // добавляем роли
            Role adminRole = new Role { RoleId = 1, Name = adminRoleName };
            Role userRole = new Role { RoleId = 2, Name = userRoleName };
            User adminUser = new User { Id = 1, Username = adminEmail, Password = adminPassword, RoleId = adminRole.RoleId };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}
