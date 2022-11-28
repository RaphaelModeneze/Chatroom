using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;

namespace Chatroom.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ChatMessage> ChatMessage { get; set; }
        public DbSet<ChatRoom> ChatRoom { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}