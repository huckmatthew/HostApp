using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HostApp.Core.Utility
{
    public static class AssemblyHelper
    {
        public static IEnumerable<Type> GetImplementations(Type interfaceType)
        {
            // this will load the types for all of the currently loaded assemblies in the
            // current domain.
            return FindInterface(interfaceType);
        }

        public static IEnumerable<Type> GetImplementations(Type interfaceType, string assemblyName)
        {
            var currentPath = Directory.GetCurrentDirectory();
            var fullpath = Path.Combine(currentPath, string.Format("{0}.dll", assemblyName));
            var assembly = Assembly.LoadFile(fullpath);
            var types = assembly.GetExportedTypes().Where(
                    t => interfaceType.IsAssignableFrom(t) && t.IsClass).ToArray();
            return types;

        }

        public static IEnumerable<Type> FindInterface(Type interfaceType)
        {
            var foundTypes = new List<Type>();
            var currentPath = Directory.GetCurrentDirectory();
            var dllFiles = Directory.EnumerateFiles(currentPath, "*.dll", SearchOption.TopDirectoryOnly).ToList();
            foreach (var f in dllFiles)
            {
                try
                {
                    var assembly = Assembly.LoadFile(f);
                    var types = assembly.GetExportedTypes().Where(
                        t => interfaceType.IsAssignableFrom(t) && t.IsClass).ToArray();
                    if (types.Any())
                        foundTypes.AddRange(types);

                }
                catch (Exception)
                {
                        
                }
            }
            return foundTypes;
        }

    }
}
