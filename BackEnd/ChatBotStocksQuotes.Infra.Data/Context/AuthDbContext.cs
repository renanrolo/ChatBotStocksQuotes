using ChatBotStocksQuotes.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatBotStocksQuotes.Infra.Data.Context
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
      : base(options)
        { }

        public DbSet<Chat> Chats { get; set; }
    }
}
