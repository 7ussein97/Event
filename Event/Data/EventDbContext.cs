using Microsoft.EntityFrameworkCore;
using Event.Models;
namespace Event.Data
{
    public class EventDbContext: DbContext
    {
        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options) { }

        public DbSet<EventData> Events { get; set; }
    }
}
