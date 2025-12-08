using Microsoft.EntityFrameworkCore;
using MojaApp.API.Models;

namespace MojaApp.API.Data
{
    public class MyDBContext : DbContext // ctrl + . using entity framework core
    {
        public DbSet<Student> Student => Set<Student>();
        public DbSet<Opstina> Opstina => Set<Opstina>();

        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {

        }
    }
}
