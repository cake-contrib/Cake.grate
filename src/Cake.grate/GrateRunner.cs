using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.grate
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
        public GrateRunner(
            IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools,
            ICakeLog log)
            : base(fileSystem, environment, processRunner, tools)
        {
            this.fileSystem = fileSystem;
            this.tools = tools;
            this.environment = environment;
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

            Run(settings, GetArguments(settings));
        }

        /// <inheritdoc cref="Tool{TSettings}.GetToolExecutableNames(TSettings)"/>
        protected override IEnumerable<string> GetToolExecutableNames()
        {
            if (environment.Platform.Family == PlatformFamily.Windows)
            {
                return new[] { "dotnet.exe" };
            }

            return new[] { "dotnet" };
        }

        /// <inheritdoc cref="Tool{TSettings}.GetToolName"/>
        protected override string GetToolName()
        {
            return "grate";
        }

        private ProcessArgumentBuilder GetArguments(GrateSettings settings)
        {
            var builder = new ProcessArgumentBuilder();

            builder.Append("grate");


            return builder;
        }
    }
}
