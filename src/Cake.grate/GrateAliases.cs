﻿// MIT License
//
// Copyright (c) 2023 Fran Hoey
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Grate
{
    // TODO: Update this documentation
    /// <summary>
    /// <para>Contains functionality related to <see href="https://erikbra.github.io/grate/">grate</see>.</para>
    /// <para>
    /// In order to use the commands for this alias, include the following in your build.cake file to download and
    /// install from nuget.org, or specify the ToolPath within the <see cref="GrateSettings" /> class:
    /// <code>
    /// #tool "nuget:?package=roundhouse"
    /// </code>
    /// </para>
    /// </summary>
    [CakeAliasCategory("grate")]
    public static class GrateAliases
    {
        // TODO: Update this documentation
        /// <summary>
        /// Executes grate with the given configured settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
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

            var runner = new GrateRunner(
                context.FileSystem,
                context.Environment,
                context.ProcessRunner,
                context.Tools,
                context.Log);
            runner.Run(settings);
        }

        // TODO: Update this documentation
        /// <summary>
        /// Executes grate migration to drop the database using the provided settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
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

            // TODO: Update the call to the Runner
            // var runner = new RoundhouseRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            // runner.Run(settings, true);
        }
    }
}
