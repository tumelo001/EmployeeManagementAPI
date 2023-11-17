using Employee_Management.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management.Data
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 
        
        public DbSet<Employee> Employees { get; set; }  
        public DbSet<Position> Positions { get; set; }  
        public DbSet<Department> Department { get; set; }  
    }
}
