using Microsoft.EntityFrameworkCore;
using PeopleAPI.Models;

namespace PeopleAPI {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<People> Peoples { get; set; }
    }
}
