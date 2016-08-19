using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostApp.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatWith(this string source, params object[] args)
        {
            return args == null || args.Length == 0 ? source : string.Format(source, args);

        }

        public static bool IsNullOrWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// Get the Left most characters of a string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Left(this string source, int length)
        {
            if (source == null)
            {
                return source;
            }

            if (length > source.Length)
            {
                length = source.Length;
            }

            return source.Substring(0, length);
        }


    }
}
