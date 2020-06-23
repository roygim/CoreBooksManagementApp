using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksManagement.API.Models
{
    public class UsersDbContext: IdentityDbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        { }
    }
}
