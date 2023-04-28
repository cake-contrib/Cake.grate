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
            //fixture.GivenToolExists();

            var actual = fixture.Run();

            actual.Path.FullPath.Should().Contain("grate");
        }
    }
}
