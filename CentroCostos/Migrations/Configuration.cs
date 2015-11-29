namespace CentroCostos.Migrations
{
    using CentroCostos.Infrastructure.Services;
    using CentroCostos.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CentroCostos.Infrastructure.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CentroCostos.Infrastructure.ApplicationContext context)
        {
            if (context.Usuarios.ToList().Count == 0)
            {
                UserService service = new UserService(context);

                service.CreateUser("admin", "admin12");
            }
        }
    }
}
