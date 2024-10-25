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
        /// <param name="key">The name of the user token (-usertokens [key]=[value]).</param>
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
