using Cake.Core;
using Cake.Core.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.grate
{
    /// <summary>
    /// <para>Contains functionality related to <see href="https://erikbra.github.io/grate/">grate</see>.</para>
    /// <para>
    //TODO: Update this documentation
    /// In order to use the commands for this alias, include the following in your build.cake file to download and
    /// install from nuget.org, or specify the ToolPath within the <see cref="RoundhouseSettings" /> class:
    /// <code>
    /// #tool "nuget:?package=roundhouse"
    /// </code>
    /// </para>
    /// </summary>
    [CakeAliasCategory("grate")]
    public static class GrateAliases
    {

        /// <summary>
        /// Executes grate with the given configured settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        //TODO: Update this documentation
        /// RoundhouseMigrate(new RoundhouseSettings{
        ///     ServerName = "Sql2008R2",
        ///     DatabaseName = "AdventureWorks2008R2",
        ///     SqlFilesDirectory = "./src/sql"
        ///     });
        /// </code>
        /// </example>
        [CakeMethodAlias]
        public static void GrateMigrate(this ICakeContext context, GrateSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            //TODO: Update the call to the Runner
            //var runner = new RoundhouseRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            //runner.Run(settings, settings.Drop);
        }

        /// <summary>
        /// Executes grate migration to drop the database using the provided settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        //TODO: Update this documentation
        /// RoundhouseDrop(new RoundhouseSettings{
        ///     ServerName = "Sql2008R2",
        ///     DatabaseName = "AdventureWorks2008R2"
        ///     });
        /// </code>
        /// </example>
        [CakeMethodAlias]
        public static void GrateDrop(this ICakeContext context, GrateSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            //TODO: Update the call to the Runner
            //var runner = new RoundhouseRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            //runner.Run(settings, true);
        }
    }
}
