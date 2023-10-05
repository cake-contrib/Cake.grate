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

using Cake.Core;
using Cake.Grate.Tests.Fixtures;
using Cake.Testing;

using FluentAssertions;
using Xunit;

namespace Cake.Grate.Tests
{
    public class GrateRunnerTests
    {
        private readonly GrateRunnerFixture fixture;

        public GrateRunnerTests()
        {
            fixture = new GrateRunnerFixture();
        }

        [Fact]
        public void Should_Throw_If_Settings_Are_Null()
        {
            fixture.Settings = null;

            Action result = () => fixture.Run();

            result.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Should_Throw_If_Log_Is_Null()
        {
            fixture.Log = null;

            Action result = () => fixture.Run();

            result.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Should_add_grate_when_ever_tool_is_run()
        {
            fixture.GivenConnectionStringSpecified();

            var actual = fixture.Run();

            actual.Path.FullPath.Should().Contain("grate");
        }

        [Fact]
        public void Should_throw_exception_if_connectionstring_is_not_set()
        {
            fixture.Invoking(f => f.Run())
                .Should().Throw<CakeException>();
        }


        [Fact]
        public void Should_Execute_Process_With_Flags()
        {
            // Given
            fixture.GivenConnectionStringSpecified();
            fixture.Settings.Drop = true;
            fixture.Settings.DryRun = true;
            fixture.Settings.Silent = true;
            fixture.Settings.WarnOnOneTimeScriptChanges = true; //TODO add to demo script after folders are done
            fixture.Settings.WarnAndIgnoreOnOneTimeScriptChanges = true; //TODO add to demo script after folders are done
            fixture.Settings.WithTransaction = true;
            fixture.Settings.Baseline = true;
            fixture.Settings.RunAllAnyTimeScripts = true; //TODO add to demo script after folders are done
            fixture.Settings.DisableTokenReplacement = true; //TODO add to demo script after folders are done
            fixture.Settings.RunAllAnyTimeScripts = true; //TODO add to demo script after folders are done
            fixture.Settings.DoNotStoreScriptsRunText = true;

            // When
            var result = fixture.Run();

            // Then
            result.Args.Should().StartWith("--drop --dryrun --silent --baseline --disabletokens --runallanytimescripts -warnononetimescriptchanges --warnandignoreononetimescriptchanges --transaction --donotstorescriptsruntext");
        }

        [Fact]
        public void Should_Execute_Process_With_Database_Settings()
        {
            // Given
            fixture.GivenConnectionStringSpecified();
            fixture.Settings.CommandTimeout = 12;
            fixture.Settings.CommandTimeoutAdmin = 23;
            fixture.Settings.ConnectionString = "server=foo;db=bar";
            fixture.Settings.ConnectionStringAdmin = "server=fooAd;db=barAd";
            fixture.Settings.Restore = "/backs/restore";
            fixture.Settings.SchemaName = "RH";
            fixture.Settings.AccessToken = "ac";

            // When
            var result = fixture.Run();

            // Then
            result.Args.Should().EndWith("\"--commandtimeout=12\" \"--admincommandtimeout=23\" \"--connectionstring=server=foo;db=bar\" " +
                         "\"--adminconnectionstring=server=fooAd;db=barAd\" " +
                         "\"--restore=/backs/restore\" " +
                         "\"--schemaname=RH\" \"-accesstoken=ac\"");
        }

        [Fact]
        public void Should_Execute_Process_With_Roundhouse_Settings() // TODO: Add to demo script
        {
            // Given
            fixture.GivenConnectionStringSpecified();
            fixture.Settings.DatabaseType = "roundhouse.databases.postgresql";
            fixture.Settings.Environment = "STAGING";
            fixture.Settings.OutputPath = "out_path";
            fixture.Settings.SqlFilesDirectory = "/db/scripts";
            fixture.Settings.Version = "1.1.1.1";

            // When
            var result = fixture.Run();

            // Then
            result.Args.Should().EndWith("\"--databasetype=roundhouse.databases.postgresql\" " +
                         "\"--environment=STAGING\" \"--outputPath=out_path\" " +
                         "\"-sqlfilesdirectory=/db/scripts\" \"-version=1.1.1.1\"");
        }
    }
}
