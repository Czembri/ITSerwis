using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ItSerwis_Merge_v2.Utils
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Encryptor>().As<IEncryptor>();
            builder.RegisterAssemblyTypes(Assembly.Load(nameof(ItSerwis_Merge_v2)))
                .Where(t => t.Namespace.Contains("Utils"))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));

            return builder.Build();
        }
    }
}
