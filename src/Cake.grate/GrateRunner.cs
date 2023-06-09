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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Grate
{
    /// <summary>
    /// The Runner, to run the tool.
    /// </summary>
    public class GrateRunner : Tool<GrateSettings>
    {
        private readonly ICakeEnvironment environment;
        private readonly IToolLocator tools;
        private readonly IFileSystem fileSystem;
        private readonly ICakeLog log;

        /// <summary>
        /// Initializes a new instance of the <see cref="GrateRunner"/> class.
        /// </summary>
        /// <param name="fileSystem">An <see cref="IFileSystem"/>.</param>
        /// <param name="environment">An <see cref="ICakeEnvironment"/>.</param>
        /// <param name="processRunner">An <see cref="IProcessRunner"/>.</param>
        /// <param name="tools">An <see cref="IToolLocator"/>.</param>
        /// <param name="log">An <see cref="ICakeLog"/>.</param>
        public GrateRunner(
            IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools,
            ICakeLog log)
            : base(fileSystem, environment, processRunner, tools)
        {
            if (processRunner is null)
            {
                throw new ArgumentNullException(nameof(processRunner));
            }

            this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
            this.tools = tools ?? throw new ArgumentNullException(nameof(tools));
            this.environment = environment ?? throw new ArgumentNullException(nameof(environment));
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Runs the tool.
        /// </summary>
        /// <param name="settings">The settings to run with.</param>
        public void Run(GrateSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            ValidateSettings(settings);
            Run(settings, GetArguments(settings));
        }

        /// <inheritdoc cref="Tool{TSettings}.GetToolExecutableNames(TSettings)"/>
        protected override IEnumerable<string> GetToolExecutableNames()
        {
            if (environment.Platform.Family == PlatformFamily.Windows)
            {
                return new[] { "grate.exe" };
            }

            return new[] { "grate" };
        }

        /// <inheritdoc cref="Tool{TSettings}.GetToolName"/>
        protected override string GetToolName()
        {
            return "grate";
        }

        private void ValidateSettings(GrateSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.ConnectionString))
            {
                throw new CakeException("GrateSetting 'ConnectionString' is required.");
            }
        }

        private ProcessArgumentBuilder GetArguments(GrateSettings settings)
        {
            var builder = new ProcessArgumentBuilder();

            AddFolderArguments(builder, settings);
            AddFlagArguments(builder, settings);
            AddDatabaseArguments(builder, settings);
            AddGrateArguments(builder, settings);

            return builder;
        }

        private void AddFolderArguments(ProcessArgumentBuilder builder, GrateSettings settings)
        {
            //TODO: Use grate folder configuration https://erikbra.github.io/grate/folder-configuration/
        }

        private void AddFlagArguments(ProcessArgumentBuilder builder, GrateSettings settings)
        {
            AppendFlag(builder, "drop", settings.Drop);
            AppendFlag(builder, "dryrun", settings.DryRun);
            AppendFlag(builder, "silent", settings.Silent);
            AppendFlag(builder, "baseline", settings.Baseline);
            //TODO: Add usertokens (ut)
            AppendFlag(builder, "disabletokens", settings.DisableTokenReplacement);
            AppendFlag(builder, "runallanytimescripts", settings.RunAllAnyTimeScripts);
            AppendFlag(builder, "w", settings.WarnOnOneTimeScriptChanges);
            AppendFlag(builder, "warnandignoreononetimescriptchanges", settings.WarnAndIgnoreOnOneTimeScriptChanges);
            AppendFlag(builder, "t", settings.WithTransaction);
            AppendFlag(builder, "donotstorescriptsruntext", settings.DoNotStoreScriptsRunText);
        }

        private void AddDatabaseArguments(ProcessArgumentBuilder builder, GrateSettings settings)
        {
            AppendQuotedIfExists(builder, "ct", settings.CommandTimeout);
            AppendQuotedIfExists(builder, "cta", settings.CommandTimeoutAdmin);
            AppendQuotedSecretIfExists(builder, "cs", settings.ConnectionString);
            AppendQuotedSecretIfExists(builder, "csa", settings.ConnectionStringAdmin);
            AppendQuotedIfExists(builder, "restore", settings.Restore);
            AppendQuotedIfExists(builder, "sc", settings.SchemaName);
            AppendQuotedIfExists(builder, "accesstoken", settings.AccessToken);
        }

        private void AddGrateArguments(ProcessArgumentBuilder builder, GrateSettings settings)
        {
            AppendQuotedIfExists(builder, "dt", settings.DatabaseType);
            AppendQuotedIfExists(builder, "env", settings.Environment);
            AppendQuotedIfExists(builder, "o", settings.OutputPath);
            AppendQuotedIfExists(builder, "f", settings.SqlFilesDirectory);
            AppendQuotedIfExists(builder, "version", settings.Version);
            //TOOD: Add Verbosity
        }

        private void AppendFlag(ProcessArgumentBuilder builder, string key, bool value)
        {
            if (value)
            {
                builder.Append("-{0}", key);
            }
        }

        private void AppendQuotedIfExists(ProcessArgumentBuilder builder, string key, object value)
        {
            if (value != null)
            {
                builder.AppendQuoted("-{0}={1}", key, value);
            }
        }

        private void AppendQuotedSecretIfExists(ProcessArgumentBuilder builder, string key, object value)
        {
            if (value != null)
            {
                builder.AppendQuotedSecret("-{0}={1}", key, value);
            }
        }
    }
}
