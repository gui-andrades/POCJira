using System.Reflection;
using Autofac;
using POCJira.Shared.Contexts;
using POCJira.Shared.Contexts.Base;

namespace POCJira.Shared.Extensions
{
    public static class AutoFacExtension
    {
        public static void AddAutofacServiceProvider(this ContainerBuilder builder)
        {
            builder.RegisterDbContext();
            builder.RegisterShared();
            builder.RegisterDomain();
            builder.RegisterRepository();
        }

        private static void RegisterDbContext(this ContainerBuilder builder)
        {
            builder.RegisterType<DbContext>().InstancePerLifetimeScope();
        }

        private static void RegisterShared(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("POCJira.Shared")).AsImplementedInterfaces().InstancePerLifetimeScope();
        }

        private static void RegisterDomain(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("POCJira.Domain")).AsImplementedInterfaces().InstancePerLifetimeScope();
        }

        private static void RegisterRepository(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("POCJira.Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();
        }

    }
}
