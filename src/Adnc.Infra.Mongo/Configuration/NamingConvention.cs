using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Mongo.Configuration
{
    public enum NamingConvention
    {
        /// <summary>
        /// Convert names to "lowercase" without word separators.
        /// </summary>
        LowerCase,

        /// <summary>
        /// Convert names to "UPPERCASE" without word separators.
        /// </summary>
        UpperCase,

        /// <summary>
        /// Convert names to "UpperCamelCase".
        /// </summary>
        Pascal,

        /// <summary>
        /// Convert names to "camelCase".
        /// </summary>
        Camel,

        /// <summary>
        /// Convert names to "snake_case".
        /// </summary>
        Snake
    }
}
