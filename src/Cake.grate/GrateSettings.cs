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

using Cake.Core.Tooling;

namespace Cake.Grate
{
    /// <summary>
    /// Contains settings used by <see cref="GrateRunner" />.
    /// </summary>
    public sealed class GrateSettings : ToolSettings
    {

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>
        /// You now provide an entire connection string. ServerName and Database from grate are obsolete.
        /// </value>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the connection string for admin connections.
        /// </summary>
        /// <value>
        /// Used when creating a new database, rather than migrating an existing one.
        /// </value>
        public string ConnectionStringAdmin { get; set; }

        /// <summary>
        /// Gets or sets the timeout (in seconds) for normal connections.
        /// </summary>
        /// <value>
        /// This is the timeout when commands are run. This is not for admin commands or restore.
        /// </value>
        public int? CommandTimeout { get; set; }

        /// <summary>
        /// Gets or sets the timeout (in seconds) for admin connections.
        /// </summary>
        /// <value>
        /// This is the timeout when administration commands are run (except for restore, which has its own)
        /// </value>
        public int? CommandTimeoutAdmin { get; set; }

        /// <summary>
        /// Gets or sets the sql files directory.
        /// </summary>
        /// <value>
        /// The directory where your SQL scripts are located.
        /// </value>
        public string SqlFilesDirectory { get; set; }

        /// <summary>
        /// Gets or sets the Folder configuration. See <see href="https://erikbra.github.io/grate/folder-configuration/">Grate website</see> for documentation.
        /// </summary>
        /// <value>
        /// The folder configuration.
        /// </value>
        public string Folders { get; set; }

        /// <summary>
        /// Gets or sets the version file.
        /// </summary>
        /// <value>
        /// Database Version - specify the version of the current migration directly on the command line.
        /// </value>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the schema name to use instead of [grate].
        /// </summary>
        /// <value>
        /// The schema where grate stores its tables. If migrating from RoundHouse use "RoundhousE".
        /// </value>
        public string SchemaName { get; set; }

        /// <summary>
        /// Gets or sets the environment for grate to be scoped.
        /// </summary>
        /// <value>
        /// Environment Name - This allows grate to be environment aware and only run scripts that are in a particular environment based on the name of the script. ‘something.ENV.LOCAL.sql’ would only be run if –env=LOCAL was set.
        /// </value>
        public string Environment { get; set; }

        /// <summary>
        /// Gets or sets the restore file path.
        /// </summary>
        /// <value>
        /// This instructs grate where to find the database backup file (.bak) to restore from. If this option is not specified, no restore will be done.
        /// </value>
        public string Restore { get; set; }

        /// <summary>
        /// Gets or sets the output path.
        /// </summary>
        /// <value>
        /// This is where everything related to the migration is stored. This includes any backups, all items that ran, permission dumps, logs, etc. Default is %LOCALAPPDATA%/grate
        /// </value>
        public string OutputPath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to warn when previously run scripts have changed.
        /// </summary>
        /// <value>
        /// Instructs grate to execute changed one time scripts(DDL / DML in Upfolder) that have previously been run against the database instead of failing. A warning is logged for each one time script that is rerun.
        /// </value>
        public bool WarnOnOneTimeScriptChanges { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to warn and update when previously run one time run scripts.
        /// </summary>
        /// <value>
        /// Instructs grate to ignore and update the hash of changed one time scripts (DDL/DML in Up folder) that have previously been run against the database instead of failing. A warning is logged for each one time scripts that is rerun.
        /// </value>
        public bool WarnAndIgnoreOnOneTimeScriptChanges { get; set; }

        /// <summary>
        ///  Gets or sets a value indicating whether to keep grate silent.
        /// </summary>
        /// <value>
        /// Tells grate not to ask for any input when it runs.
        /// </value>
        public bool Silent { get; set; }

        /// <summary>
        /// Gets or sets database type.
        /// </summary>
        /// <value>
        /// Tells grate what type of database it is running on.
        /// </value>
        public string DatabaseType { get; set; }

        /// <summary>
        ///  Gets or sets a value indicating whether to drop the DB.
        /// </summary>
        /// <value>
        ///  This instructs grate to remove the target database. Unlike RoundhousE grate will continue to run the migration scripts after the drop.
        /// </value>
        public bool Drop { get; set; }

        /// <summary>
        ///  Gets or sets a value indicating whether to use transactions.
        /// </summary>
        /// <value>
        ///  Run the migration in a transaction
        /// </value>
        public bool WithTransaction { get; set; }

        /// <summary>
        ///  Gets or sets a value indicating whether to perform a dry run.
        /// </summary>
        /// <value>
        /// This instructs grate to log what would have run, but not to actually run anything against the database. Use this option if you are trying to figure out what grate is going to do.
        /// </value>
        public bool DryRun { get; set; }

        /// <summary>
        ///  Gets or sets a value indicating whether to create an insert for its recording tables, but not run anything.
        /// </summary>
        /// <value>
        /// This instructs grate to mark the scripts as run, but not to actually run anything against the database. Use this option if you already have scripts that have been run through other means (and BEFORE you start the new ones). Defaults to <c>false</c>.
        /// </value>
        public bool Baseline { get; set; }

        /// <summary>
        ///  Gets or sets a value indicating whether to execute any time scripts.
        /// </summary>
        /// <value>
        /// This instructs grate to run any time scripts every time it is run even if they haven’t changed. Defaults to <c>false</c>.
        /// </value>
        public bool RunAllAnyTimeScripts { get; set; }

        /// <summary>
        ///  Gets or sets a value indicating whether to perform token replacement.
        /// </summary>
        /// <value>
        /// This instructs grate to not perform token replacement {{somename}}. Defaults to <c>false</c>.
        /// </value>
        public bool DisableTokenReplacement { get; set; }

        /// <summary>
        ///  Gets or sets the access token to use
        /// </summary>
        /// <value>
        /// Specify an access token to use when connecting to SQL Server.
        /// </value>
        public string AccessToken { get; set; }

        /// <summary>
        ///  Gets or sets a value indicating whether to store script text.
        /// </summary>
        /// <value>
        /// This instructs grate to not store the full script text in the database. Defaults to <c>false</c>.
        /// </value>
        public bool DoNotStoreScriptsRunText { get; set; }

    }
}
