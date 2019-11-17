namespace TCC.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TCC.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TCC.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TCC.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

            context.Users.AddOrUpdate(
                x => x.Email,  //Using Email as the Unique Key: If a record exists with the same email, AddOrUpdate skips it.
                new ApplicationUser() { Email = "admin@pet.com", UserName = "Administrador", PasswordHash = new PasswordHasher().HashPassword("P@ssw0rd") }
            );
            
            context.Roles.AddOrUpdate(
                new IdentityRole() { Id = "Administrador", Name = "Administrador"},   
                new IdentityRole() { Id = "Usuário", Name = "Usuário" }
            );

            context.SaveChanges();

            var user = userManager.FindByName("Administrador");

            userManager.AddToRole(user.Id, "Administrador");

            context.SaveChanges();

            //Get the UserId only if the SecurityStamp is not set yet.
            string userId = context.Users.Where(x => x.Email == "admin@pet.com" && string.IsNullOrEmpty(x.SecurityStamp)).Select(x => x.Id).FirstOrDefault();

            //If the userId is not null, then the SecurityStamp needs updating.
            if (!string.IsNullOrEmpty(userId)) userManager.UpdateSecurityStamp(userId);
        }
    }
}
