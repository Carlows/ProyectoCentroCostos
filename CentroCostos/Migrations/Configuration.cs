namespace CentroCostos.Migrations
{
    using CentroCostos.Infrastructure.Services;
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
            UserService service = new UserService(context);

            service.CreateUser("carlos", "carlos12");
        }
    }
}
