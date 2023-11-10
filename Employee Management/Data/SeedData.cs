using Microsoft.EntityFrameworkCore;

namespace Employee_Management.Data
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices.CreateScope()
                                       .ServiceProvider.GetRequiredService<AppDbContext>(); 

            if(context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();

            if (!context.Positions.Any())
            {

                context.SaveChanges();
            }
            if (!context.Employees.Any())
            {

            }


            //Manager, Supervisor, Analyst, Specialist, Associate
            context.SaveChanges();
        }
    }
}
