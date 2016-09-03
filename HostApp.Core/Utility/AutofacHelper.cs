using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;

namespace HostApp.Core.Utility
{
    public static class AutofacHelper
    {
        public static void RegisterDLL(ContainerBuilder builder, string dllName, string patternMatch)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (String.IsNullOrWhiteSpace(path))
            {
                return;
            }

            var assemblies = Directory.GetFiles(path, dllName, SearchOption.TopDirectoryOnly)
                                      .Select(Assembly.LoadFrom);

            foreach (var assembly in assemblies)
            {
                if (assembly.GetTypes().Any(p => p.Name.EndsWith(patternMatch)))
                {
                    builder.RegisterAssemblyTypes(assembly)
                        .Where(t => t.Name.EndsWith(patternMatch) && !t.IsInterface)
                        .AsImplementedInterfaces();

                }
            }
        }


    }
}
