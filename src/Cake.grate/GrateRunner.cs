// MIT License
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
        private readonly Verbosity verbosity;

        /// <summary>
        /// Initializes a new instance of the <see cref="GrateRunner"/> class.
        /// </summary>
        /// <param name="fileSystem">An <see cref="IFileSystem"/>.</param>
        /// <param name="environment">An <see cref="ICakeEnvironment"/>.</param>
        /// <param name="processRunner">An <see cref="IProcessRunner"/>.</param>
        /// <param name="tools">An <see cref="IToolLocator"/>.</param>
        /// <param name="verbosity">The verbosity or the logging</param>
        public GrateRunner(
            IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools,
            Verbosity verbosity)
            : base(fileSystem, environment, processRunner, tools)
        {
            if (processRunner is null)
            {
                throw new ArgumentNullException(nameof(processRunner));
            }

            this.environment = environment ?? throw new ArgumentNullException(nameof(environment));
            this.verbosity = verbosity;
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
            Run(settings, GetArguments(settings, verbosity));
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

        private static void ValidateSettings(GrateSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.ConnectionString))
            {
                throw new CakeException("GrateSetting 'ConnectionString' is required.");
            }
        }

        private static ProcessArgumentBuilder GetArguments(GrateSettings settings, Verbosity verbosity)
        {
            var builder = new ProcessArgumentBuilder();

            AddFlagArguments(builder, settings);
            AddDatabaseArguments(builder, settings);
            AddGrateArguments(builder, settings, verbosity);
            AddUserTokenArguments(builder, settings);

            return builder;
        }

        private static void AddFlagArguments(ProcessArgumentBuilder builder, GrateSettings settings)
        {
            AppendFlag(builder, "drop", settings.Drop);
            AppendFlag(builder, "dryrun", settings.DryRun);
            AppendFlag(builder, "silent", settings.Silent);
            AppendFlag(builder, "baseline", settings.Baseline);
            AppendFlag(builder, "disabletokens", settings.DisableTokenReplacement);
            AppendFlag(builder, "runallanytimescripts", settings.RunAllAnyTimeScripts);
            AppendFlag(builder, "warnononetimescriptchanges", settings.WarnOnOneTimeScriptChanges);
            AppendFlag(builder, "warnandignoreononetimescriptchanges", settings.WarnAndIgnoreOnOneTimeScriptChanges);
            AppendFlag(builder, "transaction", settings.WithTransaction);
            AppendFlag(builder, "donotstorescriptsruntext", settings.DoNotStoreScriptsRunText);
            AppendFlag(builder, "isuptodate", settings.IsUpToDate);
        }

        private static void AddDatabaseArguments(ProcessArgumentBuilder builder, GrateSettings settings)
        {
            AppendQuotedIfExists(builder, "commandtimeout", settings.CommandTimeout);
            AppendQuotedIfExists(builder, "admincommandtimeout", settings.CommandTimeoutAdmin);
            AppendQuotedSecretIfExists(builder, "connectionstring", settings.ConnectionString);
            AppendQuotedSecretIfExists(builder, "adminconnectionstring", settings.ConnectionStringAdmin);
            AppendQuotedIfExists(builder, "restore", settings.Restore);
            AppendQuotedIfExists(builder, "schemaname", settings.SchemaName);
            AppendQuotedIfExists(builder, "accesstoken", settings.AccessToken);
        }

        private static void AddGrateArguments(ProcessArgumentBuilder builder, GrateSettings settings, Verbosity verbosity)
        {
            AppendQuotedIfExists(builder, "databasetype", settings.DatabaseType);
            AppendQuotedIfExists(builder, "environment", settings.Environment);
            AppendQuotedIfExists(builder, "outputPath", settings.OutputPath);
            AppendQuotedIfExists(builder, "sqlfilesdirectory", settings.SqlFilesDirectory);
            AppendQuotedIfExists(builder, "folders", settings.Folders);
            AppendQuotedIfExists(builder, "version", settings.Version);
            AppendQuotedIfExists(builder, "repositorypath", settings.RepositoryPath);
            AppendQuotedIfExists(builder, "verbosity", TranslateVerbosity(verbosity));
        }

        private static void AddUserTokenArguments(ProcessArgumentBuilder builder, GrateSettings settings)
        {
            if (settings.UserTokens.Count == 0)
            {
                return;
            }

            foreach (var userToken in settings.UserTokens)
            {
                AppendQuotedIfExists(builder, "usertokens", $"{userToken.Key}={userToken.Value}");
            }
        }

        private static void AppendFlag(ProcessArgumentBuilder builder, string key, bool value)
        {
            if (value)
            {
                builder.Append("--{0}", key);
            }
        }

        private static void AppendQuotedIfExists(ProcessArgumentBuilder builder, string key, object value)
        {
            if (value != null)
            {
                builder.AppendQuoted("--{0}={1}", key, value);
            }
        }

        private static void AppendQuotedSecretIfExists(ProcessArgumentBuilder builder, string key, object value)
        {
            if (value != null)
            {
                builder.AppendQuotedSecret("--{0}={1}", key, value);
            }
        }

        private static string TranslateVerbosity(Verbosity verbosity)
        {
            switch (verbosity)
            {
                case Verbosity.Quiet:
                    return "None";
                case Verbosity.Minimal:
                    return "Warning";
                case Verbosity.Normal:
                    return "Information";
                case Verbosity.Verbose:
                    return "Debug";
                case Verbosity.Diagnostic:
                    return "Trace";
                default:
                    return "Information"; // default used be grate
            }
        }
    }
}
