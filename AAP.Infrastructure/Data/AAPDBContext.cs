using AAP.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AAP.Infrastructure.Data
{
    public class AAPDBContext : IdentityDbContext<User, IdentityRole, string>
    {
        public AAPDBContext(DbContextOptions<AAPDBContext> options) : base(options)
        {
            
        }
    }
}
