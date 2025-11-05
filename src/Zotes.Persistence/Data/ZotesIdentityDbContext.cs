using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zotes.Persistence.Entities;

namespace Zotes.Persistence.Data;

public class ZotesIdentityDbContext(DbContextOptions<ZotesIdentityDbContext> options)
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options);