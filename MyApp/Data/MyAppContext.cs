using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.Data
{
    public class MyAppContext : DbContext
    {
        //Constructor
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options) { }

        //Set Auto Generate Table Schema to SQL Server Management
        public DbSet<Item> Items { get; set; }
    }
}
