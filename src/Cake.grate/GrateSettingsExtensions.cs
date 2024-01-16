using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Grate
{
    /// <summary>
    /// Contains functionality related to grate settings.
    /// </summary>
    public static class GrateSettingsExtensions
    {
        /// <summary>
        /// Adds a UserToken to the settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="name">The name of the user token (-usertokens [key]=[value]).</param>
        /// <param name="value">The value of the user token (-usertokens [key]=[value]).</param>
        /// <returns>The same <see cref="GrateSettings"/> instance so that multiple calls can be chained.</returns>
        public static GrateSettings WithUserToken(this GrateSettings settings, string key, string value)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            settings.UserTokens[key] = value;
            return settings;
        }
    }
}
