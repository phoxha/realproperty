using RealProperty.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace RealProperty.Data
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<User> _userManager;

        public DatabaseInitializer(DataContext dataContext, UserManager<User> userManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
        }

        public void Initialize()
        {
            _dataContext.Database.Migrate();
            if (!_dataContext.Users.Any())
            {
                var user = new User {
                    UserName = "admin",
                    Email = "admin@demo.archysoft.com",
                    EmailConfirmed = true
                };
                var result = _userManager.CreateAsync(user, "admin").Result;
                if (!result.Succeeded) {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
